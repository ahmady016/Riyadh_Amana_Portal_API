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
            _errorMessage = $"City Record with Id: {id} Not Found";
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
            _errorMessage = $"No Any Cities Records Found";
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

    public List<LookupDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<City, Guid>(),
            "deleted" => _crudService.GetList<City, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<City, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<LookupDto>>(list);
    }
    public PageResult<LookupDto> ListPage(string type, int pageSize, int pageNumber)
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
    public LookupDto FindOne(Guid id)
    {
        var city = GetCityById(id);
        return _mapper.Map<LookupDto>(city);
    }
    public List<LookupDto> FindMany(string ids)
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

    public LookupDto Add(CreateLookupInput input)
    {
        // check if any titles are existed in db
        var oldCity = _crudService.GetOne<City>(e=> e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        // if any titles existed then reject the input
        if (oldCity is not null)
        {
            _errorMessage = $"City: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // if not do the normal Add action
        var City = _mapper.Map<City>(input);
        var createdCity = _crudService.Add<City, Guid>(City);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(createdCity);
    }
    public List<LookupDto> AddMany(List<CreateLookupInput> inputs)
    {
        // get all new titles
        var titlesArList = inputs.Select(e=>e.TitleAr).ToList();
        var titlesEnList = inputs.Select(e=>e.TitleEn).ToList();

        // check if any title aleary exist in db
        var existedCities = _crudService.GetList<City, Guid>(e=> titlesArList.Contains(e.TitleAr) || titlesEnList.Contains(e.TitleEn));
        // if any new title aleary existed so reject all inputs
        if (existedCities.Count > 0)
        {
            _errorMessage = $"Cities List was rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // if all inputs titles are not exited in db do the normal add many action
        var Cities = _mapper.Map<List<City>>(inputs);
        var createdCities = _crudService.AddAndGetRange<City, Guid>(Cities);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(createdCities);
    }

    public LookupDto Update(UpdateLookupInput input)
    {
        // get old db item
        var oldCity = GetCityById(input.Id);

        // if any titles changed
        if (oldCity.TitleAr != input.TitleAr || oldCity.TitleEn != input.TitleEn ) {
            // check for its existance in db
            var CityExisted = _crudService.GetOne<City>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            // if existed reject update input
            if (CityExisted is not null) {
                _errorMessage = $"City: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        // if no titles changed or the changed ones not existed in db do the normal update
        var newCity = _mapper.Map<City>(input);
        FillRestPropsWithOldValues(oldCity, newCity);
        var updatedCity = _crudService.Update<City, Guid>(newCity);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(updatedCity);
    }
    public List<LookupDto> UpdateMany(List<UpdateLookupInput> inputs)
    {
        // get oldCities List from db
        var oldCities = GetCitiesByIds(inputs.Select(x => x.Id).ToList());

        // get inputsTitles and oldCitiesTitles
        var inputsTitlesAr = inputs.Select(e => e.TitleAr);
        var inputsTitlesEn = inputs.Select(e => e.TitleEn);
        var citiesTitlesAr = oldCities.Select(e => e.TitleAr);
        var citiesTitlesEn = oldCities.Select(e => e.TitleEn);

        // get changedCitiesTitles
        var changedCitiesTitlesAr = inputsTitlesAr
            .Where(x => !citiesTitlesAr.Contains(x))
            .ToList();
        var changedCitiesTitlesEn = inputsTitlesEn
            .Where(x => !citiesTitlesEn.Contains(x))
            .ToList();

        // if any titles changed check if aleary existed in db
        if (changedCitiesTitlesAr.Count > 0 || changedCitiesTitlesEn.Count > 0)
        {
            var CitiesExisted = _crudService.GetList<City, Guid>(e => changedCitiesTitlesAr.Contains(e.TitleAr) || changedCitiesTitlesEn.Contains(e.TitleEn));
            // if any existance found in db reject all inputs
            if (CitiesExisted.Count > 0)
            {
                _errorMessage = $"Cities List Is rejected , Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        // do the normal update many items action
        var newCities = _mapper.Map<List<City>>(inputs);

        for (int i = 0; i < oldCities.Count; i++)
            FillRestPropsWithOldValues(oldCities[i], newCities[i]);
        var updatedCities = _crudService.UpdateAndGetRange<City, Guid>(newCities);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(updatedCities);
    }

    public bool Delete(Guid id)
    {
        var City = GetCityById(id);
        _crudService.SoftDelete<City, Guid>(City);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var City = GetCityById(id);
        _crudService.Activate<City, Guid>(City);
        _crudService.SaveChanges();
        return true;
    } 
}
