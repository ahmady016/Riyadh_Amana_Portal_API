using AutoMapper;
using System.Net;

using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Partners;

public class LocalPartnerService : ILocalPartnerService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<LocalPartner> _logger;
    private string _errorMessage;

    public LocalPartnerService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<LocalPartner> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    private LocalPartner GetById(Guid id)
    {
        var localPartner = _crudService.Find<LocalPartner, Guid>(id);
        if (localPartner is null)
        {
            _errorMessage = $"LocalPartner Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return localPartner;
    }
    private List<LocalPartner> GetByIds(List<Guid> ids)
    {
        var localPartner = _crudService.GetList<LocalPartner, Guid>(e => ids.Contains(e.Id));
        if (localPartner.Count == 0)
        {
            _errorMessage = $"No Any LocalPartner Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return localPartner;
    }
    private static void FillRestPropsWithOldValues(LocalPartner oldItem, LocalPartner newItem)
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

    public List<LocalPartnerDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<LocalPartner, Guid>(),
            "deleted" => _crudService.GetList<LocalPartner, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<LocalPartner, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<LocalPartnerDto>>(list);
    }
    public PageResult<LocalPartnerDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<LocalPartner>(),
            "deleted" => _crudService.GetQuery<LocalPartner>(e => e.IsDeleted),
            _ => _crudService.GetQuery<LocalPartner>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<LocalPartnerDto>()
        {
            PageItems = _mapper.Map<List<LocalPartnerDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public LocalPartnerDto Find(Guid id)
    {
        var localPartner = GetById(id);
        return _mapper.Map<LocalPartnerDto>(localPartner);
    }
    public List<LocalPartnerDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"LocalPartner: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<LocalPartnerDto>>(list);
    }

    public LocalPartnerDto Add(CreateLocalPartnerInput input)
    {
        var localPartner = _mapper.Map<LocalPartner>(input);
        var createdLocalPartner = _crudService.Add<LocalPartner, Guid>(localPartner);
        _crudService.SaveChanges();
        return _mapper.Map<LocalPartnerDto>(createdLocalPartner);
    }
    public List<LocalPartnerDto> AddMany(List<CreateLocalPartnerInput> inputs)
    {
        var localPartners = _mapper.Map<List<LocalPartner>>(inputs);
        var createdLocalPartners = _crudService.AddAndGetRange<LocalPartner, Guid>(localPartners);
        _crudService.SaveChanges();
        return _mapper.Map<List<LocalPartnerDto>>(createdLocalPartners);
    }

    public LocalPartnerDto Update(UpdateLocalPartnerInput input)
    {
        var oldLocalPartner = GetById(input.Id);
        var newLocalPartner = _mapper.Map<LocalPartner>(input);

        FillRestPropsWithOldValues(oldLocalPartner, newLocalPartner);
        var updatedLocalPartner = _crudService.Update<LocalPartner, Guid>(newLocalPartner);
        _crudService.SaveChanges();
        
        return _mapper.Map<LocalPartnerDto>(updatedLocalPartner);
    }
    public List<LocalPartnerDto> UpdateMany(List<UpdateLocalPartnerInput> inputs)
    {
        var oldLocalPartners = GetByIds(inputs.Select(x => x.Id).ToList());
        var newLocalPartners = _mapper.Map<List<LocalPartner>>(inputs);

        for (int i = 0; i < oldLocalPartners.Count; i++)
            FillRestPropsWithOldValues(oldLocalPartners[i], newLocalPartners[i]);
        var updatedLocalPartners = _crudService.UpdateAndGetRange<LocalPartner, Guid>(newLocalPartners);
        _crudService.SaveChanges();
        
        return _mapper.Map<List<LocalPartnerDto>>(updatedLocalPartners);
    }

    public bool Delete(Guid id)
    {
        var localPartner = GetById(id);
        _crudService.SoftDelete<LocalPartner, Guid>(localPartner);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var localPartner = GetById(id);
        _crudService.Activate<LocalPartner, Guid>(localPartner);
        _crudService.SaveChanges();
        return true;
    }

}
