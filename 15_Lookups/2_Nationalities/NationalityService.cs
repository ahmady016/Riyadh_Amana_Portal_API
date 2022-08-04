using System.Linq;
using System.Net;
using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Lookups;

public class NationalityService : INationalityService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Nationality> _logger;
    private string _errorMessage;

    public NationalityService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Nationality> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    #region private Methods
    private Nationality GetNationalityById(Guid id)
    {
        var nationality = _crudService.Find<Nationality, Guid>(id);
        if (nationality is null)
        {
            _errorMessage = $"Nationality Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return nationality;
    }
    private List<Nationality> GetNationalitiesByIds(List<Guid> ids)
    {
        var nationalities = _crudService.GetList<Nationality, Guid>(e => ids.Contains(e.Id));
        if (nationalities.Count == 0)
        {
            _errorMessage = $"No Any nationality Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return nationalities;
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

    public List<LookupDto> ListNationalities(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Nationality, Guid>(),
            "deleted" => _crudService.GetList<Nationality, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Nationality, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<LookupDto>>(list);
    }
    public PageResult<LookupDto> ListNationalitiesPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Nationality>(),
            "deleted" => _crudService.GetQuery<Nationality>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Nationality>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<LookupDto>()
        {
            PageItems = _mapper.Map<List<LookupDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public LookupDto FindOneNationality(Guid id)
    {
        var nationality = GetNationalityById(id);
        return _mapper.Map<LookupDto>(nationality);
    }
    public List<LookupDto> FindManyNationalities(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"Nationality: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetNationalitiesByIds(_ids);
        return _mapper.Map<List<LookupDto>>(list);
    }

    public LookupDto AddNationality(CreateLookupInput input)
    {
        var oldNationality = _crudService.GetOne<Nationality>(e=> e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        if (oldNationality is not null)
        {
            _errorMessage = $"Nationality: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var nationality = _mapper.Map<Nationality>(input);
        var createdNationality = _crudService.Add<Nationality, Guid>(nationality);
        _crudService.SaveChanges();
        return _mapper.Map<LookupDto>(createdNationality);
    }
    public List<LookupDto> AddManyNationalities(List<CreateLookupInput> inputs)
    {
        var titleArList = inputs.Select(e=>e.TitleAr).ToList();
        var titleEnList = inputs.Select(e=>e.TitleEn).ToList();
        var nationalitiesExisted = _crudService.GetList<Nationality, Guid>(e=> titleArList.Contains(e.TitleAr) || titleEnList.Contains(e.TitleEn));
        if (nationalitiesExisted.Count != 0)
        {
            _errorMessage = $"Nationality: Nationalities List Is rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var nationalities = _mapper.Map<List<Nationality>>(inputs);
        var createdNationalities = _crudService.AddAndGetRange<Nationality, Guid>(nationalities);
        _crudService.SaveChanges();
        return _mapper.Map<List<LookupDto>>(createdNationalities);
    }

    public LookupDto UpdateNationality(UpdateLookupInput input)
    {
        var oldNationality = GetNationalityById(input.Id);
        if (oldNationality.TitleAr != input.TitleAr || oldNationality.TitleEn != input.TitleEn ) {
            var nationalityExisted = _crudService.GetOne<Nationality>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            if (nationalityExisted is not null) {
                _errorMessage = $"Nationality: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        var newNationality = _mapper.Map<Nationality>(input);
        FillRestPropsWithOldValues(oldNationality, newNationality);
        var updatedNationality = _crudService.Update<Nationality, Guid>(newNationality);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(updatedNationality);
    }
    public List<LookupDto> UpdateManyNationalities(List<UpdateLookupInput> inputs)
    {
        var oldNationalities = GetNationalitiesByIds(inputs.Select(x => x.Id).ToList());

        var oldNationalitiesTitlesAr = oldNationalities.Where(m => !inputs.Select(e => e.TitleAr).Contains(m.TitleAr)).Select(e => e.TitleAr).ToList();
        var oldNationalitiesTitleEn = oldNationalities.Where(m => !inputs.Select(e => e.TitleEn).Contains(m.TitleEn)).Select(e => e.TitleEn).ToList();
        if (oldNationalitiesTitlesAr.Count != 0 || oldNationalitiesTitleEn.Count != 0)
        {
            var nationalitiesExisted = _crudService.GetList<Nationality, Guid>(e => oldNationalitiesTitlesAr.Contains(e.TitleAr) || oldNationalitiesTitleEn.Contains(e.TitleEn));
            if (nationalitiesExisted.Count != 0)
            {
                _errorMessage = $"Nationality: Nationalities List Is rejected , Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }


        var newNationalities = _mapper.Map<List<Nationality>>(inputs);

        for (int i = 0; i < oldNationalities.Count; i++)
            FillRestPropsWithOldValues(oldNationalities[i], newNationalities[i]);
        var updatedNationalities = _crudService.UpdateAndGetRange<Nationality, Guid>(newNationalities);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(updatedNationalities);
    }

    public bool DeleteNationality(Guid id)
    {
        var nationality = GetNationalityById(id);
        _crudService.SoftDelete<Nationality, Guid>(nationality);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateNationality(Guid id)
    {
        var nationality = GetNationalityById(id);
        _crudService.Activate<Nationality, Guid>(nationality);
        _crudService.SaveChanges();
        return true;
    } 
}
