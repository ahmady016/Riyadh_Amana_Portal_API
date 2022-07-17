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
        if (refreshToken is not null) return refreshToken;

        _errorMessage = "Token Not Found !!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, HttpStatusCode.Unauthorized);
    }
    private static void RevokeRefreshToken(RefreshToken token, string ipAddress = null, string reason = null)
    {
        token.RevokedAt = DateTime.UtcNow;
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
            rf.UserId == userId && !rf.IsActive && !rf.IsValid &&
            rf.CreatedAt.AddDays(int.Parse(_config["JWT:RefreshTokenValidityInDays"])) <= DateTime.UtcNow.AddHours(3)
        );
        _crudService.SaveChanges();
    }
    private Tuple<string, RefreshToken> GenerateTokens(User user, string ipAddress = null)
    {
        // genetate accessToken and refreshToken
        var accessToken = AuthHelpers.GenerateAccessToken(user);
        var usedRefreshTokens = _crudService.GetQuery<RefreshToken>()
            .Select(r => r.Value)
            .ToList();
        var refreshToken = AuthHelpers.GenerateRefreshToken(user.Id, usedRefreshTokens, ipAddress);
        // save the new created refreshToken to db
        _crudService.Add<RefreshToken, Guid>(refreshToken);
        _crudService.SaveChanges();
        return Tuple.Create(accessToken, refreshToken);
    }

    public AuthDto Register(RegisterInput input, string ipAddress = null)
    {
        // return error if email already existed
        var existedUser = _crudService.GetOne<User>(u => u.Email == input.Email);
        if (existedUser is not null)
        {
            _errorMessage = "Email already excited !!!";
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
    public AuthDto Login(LoginDto login, string ipAddress = null)
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
    public bool Logout(Guid userId)
    {
        // return error if email already existed
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
        var user = _mapper.Map<User>(input);
        var updatedUser = _crudService.Update<User, Guid>(user);
        _crudService.SaveChanges();
        return _mapper.Map<UserDto>(updatedUser);
    }
    public List<UserDto> UpdateMany(List<UpdateUserInput> inputs)
    {
        var users = _mapper.Map<List<User>>(inputs);
        var updatedUsers = _crudService.UpdateAndGetRange<User, Guid>(users);
        _crudService.SaveChanges();
        return _mapper.Map<List<UserDto>>(updatedUsers);
    }

    public bool Delete(Guid id)
    {
        var user = _crudService.Find<User, Guid>(id);
        if (user is not null)
        {
            _crudService.SoftDelete<User, Guid>(user);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"User record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
    public bool Activate(Guid id)
    {
        var user = _crudService.Find<User, Guid>(id);
        if (user is not null)
        {
            _crudService.Activate<User, Guid>(user);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"User record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
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
        var user = _crudService.Find<User, Guid>(id);
        if (user == null)
        {
            _errorMessage = $"User Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }
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

        var _ids = ids.SplitAndRemoveEmpty(',').ToList();
        var list = _crudService.GetList<User, Guid>(e => _ids.Contains(e.Id.ToString()));
        return _mapper.Map<List<UserDto>>(list);
    }

}
