using AutoMapper;
using System.Net;

using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Partners;

public class NormalPartnerService : INormalPartnerService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<NormalPartner> _logger;
    private string _errorMessage;

    public NormalPartnerService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<NormalPartner> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    private NormalPartner GetById(Guid id)
    {
        var partner = _crudService.Find<NormalPartner, Guid>(id);
        if (partner is null)
        {
            _errorMessage = $"Partner Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return partner;
    }
    private List<NormalPartner> GetByIds(List<Guid> ids)
    {
        var partners = _crudService.GetList<NormalPartner, Guid>(e => ids.Contains(e.Id));
        if (partners.Count == 0)
        {
            _errorMessage = $"No Any Partner Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return partners;
    }
    private static void FillRestPropsWithOldValues(NormalPartner oldItem, NormalPartner newItem)
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

    public List<NormalPartnerDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Partner, Guid>(),
            "deleted" => _crudService.GetList<Partner, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Partner, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<NormalPartnerDto>>(list);
    }
    public PageResult<NormalPartnerDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Partner>(),
            "deleted" => _crudService.GetQuery<Partner>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Partner>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<NormalPartnerDto>()
        {
            PageItems = _mapper.Map<List<NormalPartnerDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public NormalPartnerDto Find(Guid id)
    {
        var partner = GetById(id);
        return _mapper.Map<NormalPartnerDto>(partner);
    }
    public List<NormalPartnerDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Partner: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<NormalPartnerDto>>(list);
    }

    public NormalPartnerDto Add(CreateNormalPartnerInput input)
    {
        var partner = _mapper.Map<NormalPartner>(input);
        var createdPartner = _crudService.Add<NormalPartner, Guid>(partner);
        _crudService.SaveChanges();
        return _mapper.Map<NormalPartnerDto>(createdPartner);
    }
    public List<NormalPartnerDto> AddMany(List<CreateNormalPartnerInput> inputs)
    {
        var partners = _mapper.Map<List<NormalPartner>>(inputs);
        var createdPartners = _crudService.AddAndGetRange<NormalPartner, Guid>(partners);
        _crudService.SaveChanges();
        return _mapper.Map<List<NormalPartnerDto>>(createdPartners);
    }

    public NormalPartnerDto Update(UpdateNormalPartnerInput input)
    {
        var oldPartner = GetById(input.Id);
        var newPartner = _mapper.Map<NormalPartner>(input);

        FillRestPropsWithOldValues(oldPartner, newPartner);
        var updatedPartner = _crudService.Update<NormalPartner, Guid>(newPartner);
        _crudService.SaveChanges();
        
        return _mapper.Map<NormalPartnerDto>(updatedPartner);
    }
    public List<NormalPartnerDto> UpdateMany(List<UpdateNormalPartnerInput> inputs)
    {
        var oldPartners = GetByIds(inputs.Select(x => x.Id).ToList());
        var newPartners = _mapper.Map<List<NormalPartner>>(inputs);

        for (int i = 0; i < oldPartners.Count; i++)
            FillRestPropsWithOldValues(oldPartners[i], newPartners[i]);
        var updatedPartners = _crudService.UpdateAndGetRange<NormalPartner, Guid>(newPartners);
        _crudService.SaveChanges();
        
        return _mapper.Map<List<NormalPartnerDto>>(updatedPartners);
    }

    public bool Delete(Guid id)
    {
        var partner = GetById(id);
        _crudService.SoftDelete<NormalPartner, Guid>(partner);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var partner = GetById(id);
        _crudService.Activate<NormalPartner, Guid>(partner);
        _crudService.SaveChanges();
        return true;
    }

}
