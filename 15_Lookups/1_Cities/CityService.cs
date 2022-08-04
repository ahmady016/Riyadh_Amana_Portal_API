using System.Linq;
using System.Net;
using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Lookups;

public class CityService : ICityService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<City> _logger;
    private string _errorMessage;

    public CityService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<City> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    #region private Methods
    private City GetCityById(Guid id)
    {
        var city = _crudService.Find<City, Guid>(id);
        if (city is null)
        {
            _errorMessage = $"city Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return city;
    }
    private List<City> GetCitiesByIds(List<Guid> ids)
    {
        var cities = _crudService.GetList<City, Guid>(e => ids.Contains(e.Id));
        if (cities.Count == 0)
        {
            _errorMessage = $"No Any citiy Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return cities;
    }

    private static void FillRestPropsWithOldValues(Lookup oldItem, Lookup newItem)
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
    #endregion

    public List<LookupDto> ListCities(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<City, Guid>(),
            "deleted" => _crudService.GetList<City, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<City, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<LookupDto>>(list);
    }
    public PageResult<LookupDto> ListCitiesPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<City>(),
            "deleted" => _crudService.GetQuery<City>(e => e.IsDeleted),
            _ => _crudService.GetQuery<City>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<LookupDto>()
        {
            PageItems = _mapper.Map<List<LookupDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public LookupDto FindOneCity(Guid id)
    {
        var city = GetCityById(id);
        return _mapper.Map<LookupDto>(city);
    }
    public List<LookupDto> FindManyCities(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"City: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetCitiesByIds(_ids);
        return _mapper.Map<List<LookupDto>>(list);
    }

    public LookupDto AddCity(CreateLookupInput input)
    {
        var oldCity = _crudService.GetOne<City>(e=> e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        if (oldCity is not null)
        {
            _errorMessage = $"City: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var City = _mapper.Map<City>(input);
        var createdCity = _crudService.Add<City, Guid>(City);
        _crudService.SaveChanges();
        return _mapper.Map<LookupDto>(createdCity);
    }
    public List<LookupDto> AddManyCities(List<CreateLookupInput> inputs)
    {
        var titleArList = inputs.Select(e=>e.TitleAr).ToList();
        var titleEnList = inputs.Select(e=>e.TitleEn).ToList();
        var CitiesExisted = _crudService.GetList<City, Guid>(e=> titleArList.Contains(e.TitleAr) || titleEnList.Contains(e.TitleEn));
        if (CitiesExisted.Count != 0)
        {
            _errorMessage = $"City: Cities List Is rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var Cities = _mapper.Map<List<City>>(inputs);
        var createdCities = _crudService.AddAndGetRange<City, Guid>(Cities);
        _crudService.SaveChanges();
        return _mapper.Map<List<LookupDto>>(createdCities);
    }

    public LookupDto UpdateCity(UpdateLookupInput input)
    {
        var oldCity = GetCityById(input.Id);
        if (oldCity.TitleAr != input.TitleAr || oldCity.TitleEn != input.TitleEn ) {
            var CityExisted = _crudService.GetOne<City>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            if (CityExisted is not null) {
                _errorMessage = $"City: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        var newCity = _mapper.Map<City>(input);
        FillRestPropsWithOldValues(oldCity, newCity);
        var updatedCity = _crudService.Update<City, Guid>(newCity);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(updatedCity);
    }
    public List<LookupDto> UpdateManyCities(List<UpdateLookupInput> inputs)
    {
        var oldCities = GetCitiesByIds(inputs.Select(x => x.Id).ToList());

        var oldCitiesTitlesAr = oldCities.Where(m => !inputs.Select(e => e.TitleAr).Contains(m.TitleAr)).Select(e => e.TitleAr).ToList();
        var oldCitiesTitleEn = oldCities.Where(m => !inputs.Select(e => e.TitleEn).Contains(m.TitleEn)).Select(e => e.TitleEn).ToList();
        if (oldCitiesTitlesAr.Count != 0 || oldCitiesTitleEn.Count != 0)
        {
            var CitiesExisted = _crudService.GetList<City, Guid>(e => oldCitiesTitlesAr.Contains(e.TitleAr) || oldCitiesTitleEn.Contains(e.TitleEn));
            if (CitiesExisted.Count != 0)
            {
                _errorMessage = $"City: Cities List Is rejected , Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }


        var newCities = _mapper.Map<List<City>>(inputs);

        for (int i = 0; i < oldCities.Count; i++)
            FillRestPropsWithOldValues(oldCities[i], newCities[i]);
        var updatedCities = _crudService.UpdateAndGetRange<City, Guid>(newCities);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(updatedCities);
    }

    public bool DeleteCity(Guid id)
    {
        var City = GetCityById(id);
        _crudService.SoftDelete<City, Guid>(City);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateCity(Guid id)
    {
        var City = GetCityById(id);
        _crudService.Activate<City, Guid>(City);
        _crudService.SaveChanges();
        return true;
    } 
}
