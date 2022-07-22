using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Partners;

public class PartnerService : IPartnerService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Partner> _logger;
    private string _errorMessage;

    public PartnerService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Partner> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    public List<PartnerDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Partner, Guid>(),
            "deleted" => _crudService.GetList<Partner, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Partner, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<PartnerDto>>(list);
    }
    public PageResult<PartnerDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Partner>(),
            "deleted" => _crudService.GetQuery<Partner>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Partner>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<PartnerDto>()
        {
            PageItems = _mapper.Map<List<PartnerDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public PartnerDto Find(Guid id)
    {
        var user = _crudService.Find<Partner, Guid>(id);
        if (user == null)
        {
            _errorMessage = $"Partner Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }
        return _mapper.Map<PartnerDto>(user);
    }
    public List<PartnerDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Partner: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').ToList();
        var list = _crudService.GetList<Partner, Guid>(e => _ids.Contains(e.Id.ToString()));
        return _mapper.Map<List<PartnerDto>>(list);
    }

    public PartnerDto Add(CreatePartnerInput input)
    {
        var partner = _mapper.Map<Partner>(input);
        var createdpartner = _crudService.Add<Partner, Guid>(partner);
        _crudService.SaveChanges();
        return _mapper.Map<PartnerDto>(createdpartner);
    }
    public List<PartnerDto> AddMany(List<CreatePartnerInput> inputs)
    {
        var partner = _mapper.Map<List<Partner>>(inputs);
        var createdpartner = _crudService.AddAndGetRange<Partner, Guid>(partner);
        _crudService.SaveChanges();
        return _mapper.Map<List<PartnerDto>>(createdpartner);
    }

    public PartnerDto Update(UpdatePartnerInput input)
    {
        var partner = _mapper.Map<Partner>(input);
        var updatedpartner = _crudService.Update<Partner, Guid>(partner);
        _crudService.SaveChanges();
        return _mapper.Map<PartnerDto>(updatedpartner);
    }
    public List<PartnerDto> UpdateMany(List<UpdatePartnerInput> inputs)
    {
        var partner = _mapper.Map<List<Partner>>(inputs);
        var updatedpartner = _crudService.UpdateAndGetRange<Partner, Guid>(partner);
        _crudService.SaveChanges();
        return _mapper.Map<List<PartnerDto>>(updatedpartner);
    }

    public bool Delete(Guid id)
    {
        var partner = _crudService.Find<Partner, Guid>(id);
        if (partner is not null)
        {
            _crudService.SoftDelete<Partner, Guid>(partner);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"Award record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
    public bool Activate(Guid id)
    {
        var partner = _crudService.Find<Partner, Guid>(id);
        if (partner is not null)
        {
            _crudService.Activate<Partner, Guid>(partner);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"Partner record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
}
