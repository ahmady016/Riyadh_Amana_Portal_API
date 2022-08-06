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
            _errorMessage = $"No Any Nationalities Records Found";
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

    public List<LookupDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Nationality, Guid>(),
            "deleted" => _crudService.GetList<Nationality, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Nationality, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<LookupDto>>(list);
    }
    public PageResult<LookupDto> ListPage(string type, int pageSize, int pageNumber)
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
    public LookupDto FindOne(Guid id)
    {
        var nationality = GetNationalityById(id);
        return _mapper.Map<LookupDto>(nationality);
    }
    public List<LookupDto> FindMany(string ids)
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

    public LookupDto Add(CreateLookupInput input)
    {
        // check if any titles are existed in db
        var oldNationality = _crudService.GetOne<Nationality>(e=> e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        // if any titles existed then reject the input
        if (oldNationality is not null)
        {
            _errorMessage = $"Nationality: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // if not do the normal Add action
        var nationality = _mapper.Map<Nationality>(input);
        var createdNationality = _crudService.Add<Nationality, Guid>(nationality);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(createdNationality);
    }
    public List<LookupDto> AddMany(List<CreateLookupInput> inputs)
    {
        // get all new titles
        var titlesArList = inputs.Select(e=>e.TitleAr).ToList();
        var titlesEnList = inputs.Select(e=>e.TitleEn).ToList();
        
        // check if any title aleary exist in db
        var existedNationalities = _crudService.GetList<Nationality, Guid>(e=> titlesArList.Contains(e.TitleAr) || titlesEnList.Contains(e.TitleEn));
        // if any new title aleary existed so reject all inputs
        if (existedNationalities.Count != 0)
        {
            _errorMessage = $"Nationalities List was rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // if all inputs titles are not existed in db do the normal add many action
        var nationalities = _mapper.Map<List<Nationality>>(inputs);
        var createdNationalities = _crudService.AddAndGetRange<Nationality, Guid>(nationalities);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(createdNationalities);
    }

    public LookupDto Update(UpdateLookupInput input)
    {
        // get old db item
        var oldNationality = GetNationalityById(input.Id);

        // if any titles changed
        if (oldNationality.TitleAr != input.TitleAr || oldNationality.TitleEn != input.TitleEn ) {
            // check for its existance in db
            var existedNationality = _crudService.GetOne<Nationality>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            // if existed reject update input
            if (existedNationality is not null) {
                _errorMessage = $"Nationality: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        // if no titles changed or the changed ones not existed in db do the normal update
        var newNationality = _mapper.Map<Nationality>(input);
        FillRestPropsWithOldValues(oldNationality, newNationality);
        var updatedNationality = _crudService.Update<Nationality, Guid>(newNationality);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(updatedNationality);
    }
    public List<LookupDto> UpdateMany(List<UpdateLookupInput> inputs)
    {
        // get oldNationalities List from db
        var oldNationalities = GetNationalitiesByIds(inputs.Select(x => x.Id).ToList());

        // get inputsTitles and oldNationalitiesTitles
        var inputsTitlesAr = inputs.Select(e => e.TitleAr);
        var inputsTitlesEn = inputs.Select(e => e.TitleEn);
        var nationalitiesTitlesAr = oldNationalities.Select(e => e.TitleAr);
        var nationalitiesTitlesEn = oldNationalities.Select(e => e.TitleEn);

        // get changedNationalitiesTitles
        var changedNationalitiesTitlesAr = inputsTitlesAr
            .Where(x => !nationalitiesTitlesAr.Contains(x))
            .ToList();
        var changedNationalitiesTitlesEn = inputsTitlesEn
            .Where(x => !nationalitiesTitlesEn.Contains(x))
            .ToList();

        // if any titles changed check if aleary existed in db
        if (changedNationalitiesTitlesAr.Count > 0 || changedNationalitiesTitlesEn.Count > 0)
        {
            var existedNationalities = _crudService.GetList<Nationality, Guid>(e => changedNationalitiesTitlesAr.Contains(e.TitleAr) || changedNationalitiesTitlesEn.Contains(e.TitleEn));
            // if any existance found in db reject all inputs
            if (existedNationalities.Count > 0)
            {
                _errorMessage = $"Nationalities List was rejected, Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        // do the normal update many items action
        var newNationalities = _mapper.Map<List<Nationality>>(inputs);

        for (int i = 0; i < oldNationalities.Count; i++)
            FillRestPropsWithOldValues(oldNationalities[i], newNationalities[i]);
        var updatedNationalities = _crudService.UpdateAndGetRange<Nationality, Guid>(newNationalities);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(updatedNationalities);
    }

    public bool Delete(Guid id)
    {
        var nationality = GetNationalityById(id);
        _crudService.SoftDelete<Nationality, Guid>(nationality);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var nationality = GetNationalityById(id);
        _crudService.Activate<Nationality, Guid>(nationality);
        _crudService.SaveChanges();
        return true;
    }

}
