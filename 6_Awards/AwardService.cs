using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Awards;

public class AwardService: IAwardService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Award> _logger;
    private string _errorMessage;

    public AwardService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Award> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    public List<AwardDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Award, Guid>(),
            "deleted" => _crudService.GetList<Award, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Award, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<AwardDto>>(list);
    }
    public PageResult<AwardDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Award>(),
            "deleted" => _crudService.GetQuery<Award>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Award>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<AwardDto>()
        {
            PageItems = _mapper.Map<List<AwardDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public AwardDto Find(Guid id)
    {
        var user = _crudService.Find<Award, Guid>(id);
        if (user == null)
        {
            _errorMessage = $"Award Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }
        return _mapper.Map<AwardDto>(user);
    }
    public List<AwardDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Award: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').ToList();
        var list = _crudService.GetList<Award, Guid>(e => _ids.Contains(e.Id.ToString()));
        return _mapper.Map<List<AwardDto>>(list);
    }

    public AwardDto Add(CreateAwardInput input)
    {
        var award = _mapper.Map<Award>(input);
        var createdaward = _crudService.Add<Award, Guid>(award);
        _crudService.SaveChanges();
        return _mapper.Map<AwardDto>(createdaward);
    }
    public List<AwardDto> AddMany(List<CreateAwardInput> inputs)
    {
        var award = _mapper.Map<List<Award>>(inputs);
        var createdaward = _crudService.AddAndGetRange<Award, Guid>(award);
        _crudService.SaveChanges();
        return _mapper.Map<List<AwardDto>>(createdaward);
    }

    public AwardDto Update(UpdateAwardInput input)
    {
        var award = _mapper.Map<Award>(input);
        var updatedaward = _crudService.Update<Award, Guid>(award);
        _crudService.SaveChanges();
        return _mapper.Map<AwardDto>(updatedaward);
    }
    public List<AwardDto> UpdateMany(List<UpdateAwardInput> inputs)
    {
        var award = _mapper.Map<List<Award>>(inputs);
        var updatedaward = _crudService.UpdateAndGetRange<Award, Guid>(award);
        _crudService.SaveChanges();
        return _mapper.Map<List<AwardDto>>(updatedaward);
    }

    public bool Delete(Guid id)
    {
        var award = _crudService.Find<Award, Guid>(id);
        if (award is not null)
        {
            _crudService.SoftDelete<Award, Guid>(award);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"Award record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
    public bool Activate(Guid id)
    {
        var award = _crudService.Find<Award, Guid>(id);
        if (award is not null)
        {
            _crudService.Activate<Award, Guid>(award);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"Award record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }

}
