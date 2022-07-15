using AutoMapper;
using DB;
using DB.Entities;
using DB.Common;
using Common;
using Dtos;

namespace Users;

public class UserService : IUserService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<User> _logger;
    private string _errorMessage;

    public UserService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<User> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
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
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound); ;
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

    public UserDto Add(CreateUserInput input)
    {
        var user = _mapper.Map<User>(input);
        var createdUser = _crudService.Add<User, Guid>(user);
        _crudService.SaveChanges();
        return _mapper.Map<UserDto>(createdUser);
    }
    public List<UserDto> AddMany(List<CreateUserInput> inputs)
    {
        var users = _mapper.Map<List<User>>(inputs);
        var createdUsers = _crudService.AddAndGetRange<User, Guid>(users);
        _crudService.SaveChanges();
        return _mapper.Map<List<UserDto>>(createdUsers);
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

}
