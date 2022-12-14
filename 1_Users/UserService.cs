using AutoMapper;
using System.Net;

using DB;
using DB.Common;
using DB.Entities;
using Common;
using Auth.Dtos;
using Dtos;
using Auth;
using Microsoft.EntityFrameworkCore;

namespace Users;

public class UserService : IUserService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<User> _logger;
    private readonly IConfiguration _config;
    private string _errorMessage;
    
    private int RefreshTokenValidityInDays => int.Parse(_config["JWT:RefreshTokenValidityInDays"]);
    
    public UserService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<User> logger,
        IConfiguration config
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
        _config = config;
    }

    private RefreshToken GetRefreshToken(string token)
    {
        var refreshToken = _crudService.GetQuery<RefreshToken>()
            .Include(rf => rf.User)
            .Where(rf => rf.Value == token)
            .FirstOrDefault();

        if (refreshToken is null)
        {
            _errorMessage = "Token Not Found !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.Unauthorized);
        }

        return refreshToken;
    }
    private static void RevokeRefreshToken(RefreshToken token, string ipAddress = null, string reason = null)
    {
        token.RevokedAt = DateTime.UtcNow.AddHours(3);
        token.RevokedIP = ipAddress;
        token.RevokedReason = reason;
    }
    private void RevokeAllUserRefreshTokens(Guid userId, string ipAddress = null)
    {
        var allUserTokens = _crudService.GetList<RefreshToken, Guid>(rf => rf.UserId == userId);
        if (allUserTokens.Count > 0)
        {
            allUserTokens.ForEach(token => RevokeRefreshToken(token, ipAddress, $"Attempted reuse of revoked token: {token}"));
            _crudService.UpdateRange<RefreshToken, Guid>(allUserTokens);
            _crudService.SaveChanges();
        }
    }
    private void RemoveOldRefreshTokens(Guid userId)
    {
        // remove old inactive refresh tokens from user based on TTL in app settings
        _crudService.GetListAndDelete<RefreshToken>(rf =>
            rf.UserId == userId && !rf.IsActive &&
            rf.CreatedAt.AddDays(RefreshTokenValidityInDays) <= DateTime.Now
        );
        _crudService.SaveChanges();
    }
    private Tuple<string, RefreshToken> GenerateTokens(User user, string ipAddress = null)
    {
        // genetate accessToken and refreshToken
        var accessToken = AuthHelpers.GenerateAccessToken(user);
        
        // get all old refresh tokens
        var usedRefreshTokens = _crudService.GetQuery<RefreshToken>()
            .Select(r => r.Value)
            .ToList();
        // generate new unique refresh token
        var refreshToken = AuthHelpers.GenerateRefreshToken(user.Id, usedRefreshTokens, ipAddress);
        
        // save the new created refreshToken to db
        _crudService.Add<RefreshToken, Guid>(refreshToken);
        _crudService.SaveChanges();
        
        // return the new created tokens
        return Tuple.Create(accessToken, refreshToken);
    }

    private User GetById(Guid id)
    {
        var user = _crudService.Find<User, Guid>(id);
        if (user == null)
        {
            _errorMessage = $"User Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }
        return user;
    }
    private List<User> GetByIds(List<Guid> ids)
    {
        var users = _crudService.GetList<User, Guid>(e => ids.Contains(e.Id));
        if (users.Count == 0)
        {
            _errorMessage = $"No Any Advertisements Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return users;
    }
    private static void FillRestPropsWithOldValues(User oldItem, User newItem)
    {
        newItem.CreatedAt = oldItem.CreatedAt;
        newItem.CreatedBy = oldItem.CreatedBy;
        newItem.UpdatedAt = oldItem.UpdatedAt;
        newItem.UpdatedBy = oldItem.UpdatedBy;
        newItem.IsActive = oldItem.IsActive;
        newItem.ActivatedAt = oldItem.ActivatedAt;
        newItem.ActivatedBy = oldItem.ActivatedBy;
        newItem.IsDeleted = oldItem.IsDeleted;
        newItem.DeletedAt = oldItem.DeletedAt;
        newItem.DeletedBy = oldItem.DeletedBy;
    }

    public AuthDto Register(RegisterInput input, string ipAddress = null)
    {
        // return error if email already existed
        var existedUser = _crudService.GetOne<User>(u => u.Email == input.Email);
        if (existedUser is not null)
        {
            _errorMessage = "Email already existed !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.Conflict);
        }

        // create new user and hash password
        var newUser = _mapper.Map<User>(input);
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);


        // save user to db
        var addedUser = _crudService.Add<User, Guid>(newUser);
        _crudService.SaveChanges();

        // genetate accessToken and refreshToken Tuple
        var tokens = GenerateTokens(addedUser, ipAddress);

        // return AuthDto with UserDto and Tokens
        return new AuthDto()
        {
            User = _mapper.Map<UserDto>(addedUser),
            AccessToken = tokens.Item1,
            RefreshToken = tokens.Item2.Value
        };
    }
    public AuthDto Login(LoginInput login, string ipAddress = null)
    {
        // return error if email or password are wrong
        var existedUser = _crudService.GetOne<User>(u => u.Email == login.Email);
        if (existedUser is null || !BCrypt.Net.BCrypt.Verify(login.Password, existedUser.Password))
        {
            _errorMessage = "Incorrect Email OR Password !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // genetate accessToken and refreshToken Tuple
        var tokens = GenerateTokens(existedUser, ipAddress);

        // return AuthDto with UserDto and Tokens
        return new AuthDto()
        {
            User = _mapper.Map<UserDto>(existedUser),
            AccessToken = tokens.Item1,
            RefreshToken = tokens.Item2.Value
        };
    }
    public AuthDto RefreshTheTokens(string token, string ipAddress)
    {
        var oldRefreshToken = GetRefreshToken(token);

        if (!oldRefreshToken.IsValid)
        {
            _errorMessage = "Invalid Token !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.Unauthorized);
        }

        // revoke all tokens of that user in case of this token has been compromised
        if (oldRefreshToken.IsRevoked) RevokeAllUserRefreshTokens(oldRefreshToken.UserId, ipAddress);

        // remove all invalid or expired tokens
        RemoveOldRefreshTokens(oldRefreshToken.User.Id);

        var tokens = GenerateTokens(oldRefreshToken.User, ipAddress);

        return new AuthDto()
        {
            User = _mapper.Map<UserDto>(oldRefreshToken.User),
            AccessToken = tokens.Item1,
            RefreshToken = tokens.Item2.Value
        };
    }
    public bool RevokeTheToken(string token, string ipAddress = null)
    {
        var oldRefreshToken = GetRefreshToken(token);
        if (!oldRefreshToken.IsValid)
        {
            _errorMessage = "Invalid Token !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.Conflict);
        }

        RevokeRefreshToken(oldRefreshToken, ipAddress, $"Revoked without replacement");
        _crudService.Update<RefreshToken, Guid>(oldRefreshToken);
        _crudService.SaveChanges();

        return true;
    }
    public bool ChangePassword(ChangePasswordInput input)
    {
        var user = GetById(input.UserId);
        if(input.NewPassword != input.ConfirmNewPassword)
        {
            _errorMessage = $"ConfirmNewPassword does't match !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        if(!BCrypt.Net.BCrypt.Verify(input.OldPassword, user.Password))
        {
            _errorMessage = $"Worng Old Password!!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(input.NewPassword);
        _crudService.Update<User, Guid>(user);
        _crudService.SaveChanges();

        return true;
    }
    public bool ChangeEmail(ChangeEmailInput input)
    {
        // get the existed user from db
        var user = GetById(input.UserId);

        // return error if old email not existed
        if (input.OldEmail != user.Email)
        {
            _errorMessage = $"Wrong Old Email !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // return error if new email already existed in db
        var existedUser = _crudService.GetOne<User>(u => u.Email == input.NewEmail);
        if (existedUser is not null)
        {
            _errorMessage = "New Email already existed !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.Conflict);
        }

        // if every things okay then do update the user with new email
        user.Email = input.NewEmail;
        _crudService.Update<User, Guid>(user);
        _crudService.SaveChanges();

        return true;
    }
    public bool Logout(Guid userId)
    {
        // return error if user not found
        var existedUser = _crudService.Find<User, Guid>(userId);
        if (existedUser is null)
        {
            _errorMessage = "User with that Id not Found !!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }

        bool foundTokens = _crudService.GetListAndDelete<RefreshToken>(r => r.UserId == userId);
        if (foundTokens)
        {
            _crudService.SaveChanges();
            return true;
        }

        _errorMessage = "User already logedout !!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
    }

    public UserDto Update(UpdateUserInput input)
    {
        var oldUser = GetById(input.Id);
        var newUser = _mapper.Map<User>(input);

        newUser.Email = oldUser.Email;
        newUser.Password = oldUser.Password;

        FillRestPropsWithOldValues(oldUser, newUser);
        var updatedUser = _crudService.Update<User, Guid>(newUser);
        _crudService.SaveChanges();

        return _mapper.Map<UserDto>(updatedUser);
    }
    public List<UserDto> UpdateMany(List<UpdateUserInput> inputs)
    {
        var oldUsers = GetByIds(inputs.Select(x => x.Id).ToList());
        var newUsers = _mapper.Map<List<User>>(inputs);

        for (int i = 0; i < oldUsers.Count; i++)
        {
            newUsers[i].Email = oldUsers[i].Email;
            newUsers[i].Password = oldUsers[i].Password;
            FillRestPropsWithOldValues(oldUsers[i], newUsers[i]);
        }
        var updatedUsers = _crudService.UpdateAndGetRange<User, Guid>(newUsers);
        _crudService.SaveChanges();

        return _mapper.Map<List<UserDto>>(updatedUsers);
    }

    public bool Delete(Guid id)
    {
        var user = GetById(id);
        _crudService.SoftDelete<User, Guid>(user);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var user = GetById(id);
        _crudService.Activate<User, Guid>(user);
        _crudService.SaveChanges();
        return true;
    }

    public List<UserDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<User, Guid>(),
            "deleted" => _crudService.GetList<User, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<User, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<UserDto>>(list);
    }
    public PageResult<UserDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<User>(),
            "deleted" => _crudService.GetQuery<User>(e => e.IsDeleted),
            _ => _crudService.GetQuery<User>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<UserDto>()
        {
            PageItems = _mapper.Map<List<UserDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public UserDto Find(Guid id)
    {
        var user = GetById(id);
        return _mapper.Map<UserDto>(user);
    }
    public List<UserDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"User: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }

        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<UserDto>>(list);
    }

}
