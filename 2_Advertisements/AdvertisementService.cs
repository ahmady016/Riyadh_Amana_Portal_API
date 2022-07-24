using AutoMapper;
using System.Net;

using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Advertisements;

public class AdvertisementService : IAdvertisementService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Advertisement> _logger;
    private string _errorMessage;

    public AdvertisementService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Advertisement> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    private Advertisement GetById(Guid id)
    {
        var advertisement = _crudService.Find<Advertisement, Guid>(id);
        if (advertisement == null)
        {
            _errorMessage = $"Advertisement Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return advertisement;
    }
    private List<Advertisement> GetByIds(List<Guid> ids)
    {
        var list = _crudService.GetList<Advertisement, Guid>(e => ids.Contains(e.Id));
        if (list.Count == 0)
        {
            _errorMessage = $"No Any Advertisements Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return list;
    }
    private static void FillRestPropsWithOldValues(Advertisement oldItem, Advertisement newItem)
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

    public List<AdvertisementDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Advertisement, Guid>(),
            "deleted" => _crudService.GetList<Advertisement, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Advertisement, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<AdvertisementDto>>(list);
    }
    public PageResult<AdvertisementDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Advertisement>(),
            "deleted" => _crudService.GetQuery<Advertisement>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Advertisement>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<AdvertisementDto>()
        {
            PageItems = _mapper.Map<List<AdvertisementDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    
    public AdvertisementDto Find(Guid id)
    {
        var advertisement = GetById(id);
        return _mapper.Map<AdvertisementDto>(advertisement);
    }
    public List<AdvertisementDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Advertisement: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<AdvertisementDto>>(list);
    }

    public AdvertisementDto Add(CreateAdvertisementInput input)
    {
        var advertisement = _mapper.Map<Advertisement>(input);
        var createdAdvertisement = _crudService.Add<Advertisement, Guid>(advertisement);
        _crudService.SaveChanges();
        return _mapper.Map<AdvertisementDto>(createdAdvertisement);
    }
    public List<AdvertisementDto> AddMany(List<CreateAdvertisementInput> inputs)
    {
        var advertisements = _mapper.Map<List<Advertisement>>(inputs);
        var createdAdvertisements = _crudService.AddAndGetRange<Advertisement, Guid>(advertisements);
        _crudService.SaveChanges();
        return _mapper.Map<List<AdvertisementDto>>(createdAdvertisements);
    }

    public AdvertisementDto Update(UpdateAdvertisementInput input)
    {
        var oldAdvertisement = GetById(input.Id);
        var newAdvertisement = _mapper.Map<Advertisement>(input);

        FillRestPropsWithOldValues(oldAdvertisement, newAdvertisement);
        var updatedAdvertisement = _crudService.Update<Advertisement, Guid>(newAdvertisement);
        _crudService.SaveChanges();
        
        return _mapper.Map<AdvertisementDto>(updatedAdvertisement);
    }
    public List<AdvertisementDto> UpdateMany(List<UpdateAdvertisementInput> inputs)
    {
        var oldAdvertisements = GetByIds(inputs.Select(x => x.Id).ToList());
        var newAdvertisements = _mapper.Map<List<Advertisement>>(inputs);
        
        for (int i = 0; i < oldAdvertisements.Count; i++)
            FillRestPropsWithOldValues(oldAdvertisements[i], newAdvertisements[i]);
        var updatedAdvertisements = _crudService.UpdateAndGetRange<Advertisement, Guid>(newAdvertisements);
        _crudService.SaveChanges();

        return _mapper.Map<List<AdvertisementDto>>(updatedAdvertisements);
    }

    public bool Delete(Guid id)
    {
        var advertisement = GetById(id);
        _crudService.SoftDelete<Advertisement, Guid>(advertisement);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var advertisement = GetById(id);
        _crudService.Activate<Advertisement, Guid>(advertisement);
        _crudService.SaveChanges();
        return true;
    }

}
