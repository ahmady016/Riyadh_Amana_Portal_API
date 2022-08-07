using AutoMapper;
using System.Net;

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

    private Partner GetById(Guid id)
    {
        var partner = _crudService.Find<Partner, Guid>(id);
        if (partner is null)
        {
            _errorMessage = $"Partner Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return partner;
    }
    private List<Partner> GetByIds(List<Guid> ids)
    {
        var partners = _crudService.GetList<Partner, Guid>(e => ids.Contains(e.Id));
        if (partners.Count == 0)
        {
            _errorMessage = $"No Any Partner Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return partners;
    }
    private static void FillRestPropsWithOldValues(Partner oldItem, Partner newItem)
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
        var partner = GetById(id);
        return _mapper.Map<PartnerDto>(partner);
    }
    public List<PartnerDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Partner: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<PartnerDto>>(list);
    }

    public PartnerDto Add(CreatePartnerInput input)
    {
        var partner = _mapper.Map<Partner>(input);
        var createdPartner = _crudService.Add<Partner, Guid>(partner);
        _crudService.SaveChanges();
        return _mapper.Map<PartnerDto>(createdPartner);
    }
    public List<PartnerDto> AddMany(List<CreatePartnerInput> inputs)
    {
        var partners = _mapper.Map<List<Partner>>(inputs);
        var createdPartners = _crudService.AddAndGetRange<Partner, Guid>(partners);
        _crudService.SaveChanges();
        return _mapper.Map<List<PartnerDto>>(createdPartners);
    }

    public PartnerDto Update(UpdatePartnerInput input)
    {
        var oldPartner = GetById(input.Id);
        var newPartner = _mapper.Map<Partner>(input);

        FillRestPropsWithOldValues(oldPartner, newPartner);
        var updatedPartner = _crudService.Update<Partner, Guid>(newPartner);
        _crudService.SaveChanges();
        
        return _mapper.Map<PartnerDto>(updatedPartner);
    }
    public List<PartnerDto> UpdateMany(List<UpdatePartnerInput> inputs)
    {
        var oldPartners = GetByIds(inputs.Select(x => x.Id).ToList());
        var newPartners = _mapper.Map<List<Partner>>(inputs);

        for (int i = 0; i < oldPartners.Count; i++)
            FillRestPropsWithOldValues(oldPartners[i], newPartners[i]);
        var updatedPartners = _crudService.UpdateAndGetRange<Partner, Guid>(newPartners);
        _crudService.SaveChanges();
        
        return _mapper.Map<List<PartnerDto>>(updatedPartners);
    }

    public bool Delete(Guid id)
    {
        var partner = GetById(id);
        _crudService.SoftDelete<Partner, Guid>(partner);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var partner = GetById(id);
        _crudService.Activate<Partner, Guid>(partner);
        _crudService.SaveChanges();
        return true;
    }

}
