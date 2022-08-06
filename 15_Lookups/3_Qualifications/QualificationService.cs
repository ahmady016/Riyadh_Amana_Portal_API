using System.Linq;
using System.Net;
using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Lookups;

public class QualificationService : IQualificationService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Qualification> _logger;
    private string _errorMessage;

    public QualificationService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Qualification> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    #region private Methods
    private Qualification GetQualificationById(Guid id)
    {
        var qualification = _crudService.Find<Qualification, Guid>(id);
        if (qualification is null)
        {
            _errorMessage = $"Qualification Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return qualification;
    }
    private List<Qualification> GetQualificationsByIds(List<Guid> ids)
    {
        var qualifications = _crudService.GetList<Qualification, Guid>(e => ids.Contains(e.Id));
        if (qualifications.Count == 0)
        {
            _errorMessage = $"No Any Qualifications Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return qualifications;
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
            "all" => _crudService.GetAll<Qualification, Guid>(),
            "deleted" => _crudService.GetList<Qualification, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Qualification, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<LookupDto>>(list);
    }
    public PageResult<LookupDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Qualification>(),
            "deleted" => _crudService.GetQuery<Qualification>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Qualification>(e => !e.IsDeleted),
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
        var qualification = GetQualificationById(id);
        return _mapper.Map<LookupDto>(qualification);
    }
    public List<LookupDto> FindMany(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"Qualification: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetQualificationsByIds(_ids);
        return _mapper.Map<List<LookupDto>>(list);
    }

    public LookupDto Add(CreateLookupInput input)
    {
        // check if any titles are existed in db
        var oldQualification = _crudService.GetOne<Qualification>(e=> e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        // if any titles existed then reject the input
        if (oldQualification is not null)
        {
            _errorMessage = $"Qualification: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // if not do the normal Add action
        var qualification = _mapper.Map<Qualification>(input);
        var createdQualification = _crudService.Add<Qualification, Guid>(qualification);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(createdQualification);
    }
    public List<LookupDto> AddMany(List<CreateLookupInput> inputs)
    {
        // get all new titles
        var titlesArList = inputs.Select(e => e.TitleAr).ToList();
        var titlesEnList = inputs.Select(e => e.TitleEn).ToList();

        // check if any title aleary exist in db
        var existedQualifications = _crudService.GetList<Qualification, Guid>(e=> titlesArList.Contains(e.TitleAr) || titlesEnList.Contains(e.TitleEn));
        if (existedQualifications.Count != 0)
        {
            _errorMessage = $"Qualifications List was rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        // if all inputs titles are not existed in db do the normal add many action
        var qualifications = _mapper.Map<List<Qualification>>(inputs);
        var createdQualifications = _crudService.AddAndGetRange<Qualification, Guid>(qualifications);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(createdQualifications);
    }

    public LookupDto Update(UpdateLookupInput input)
    {
        // get old db item
        var oldQualification = GetQualificationById(input.Id);
        
        // if any titles changed
        if (oldQualification.TitleAr != input.TitleAr || oldQualification.TitleEn != input.TitleEn ) {
            // check for its existance in db
            var existedQualification = _crudService.GetOne<Qualification>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            // if existed reject update input
            if (existedQualification is not null) {
                _errorMessage = $"Qualification: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        // if no titles changed or the changed ones not existed in db do the normal update
        var newQualification = _mapper.Map<Qualification>(input);
        FillRestPropsWithOldValues(oldQualification, newQualification);
        var updatedQualification = _crudService.Update<Qualification, Guid>(newQualification);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(updatedQualification);
    }
    public List<LookupDto> UpdateMany(List<UpdateLookupInput> inputs)
    {
        // get oldQualifications List from db
        var oldQualifications = GetQualificationsByIds(inputs.Select(x => x.Id).ToList());

        // get inputsTitles and oldQualificationsTitles
        var inputsTitlesAr = inputs.Select(e => e.TitleAr);
        var inputsTitlesEn = inputs.Select(e => e.TitleEn);
        var qualificationsTitlesAr = oldQualifications.Select(e => e.TitleAr);
        var qualificationsTitlesEn = oldQualifications.Select(e => e.TitleEn);

        // get changedQualificationsTitles
        var changedQualificationsTitlesAr = inputsTitlesAr
            .Where(x => !qualificationsTitlesAr.Contains(x))
            .ToList();
        var changedQualificationsTitlesEn = inputsTitlesEn
            .Where(x => !qualificationsTitlesEn.Contains(x))
            .ToList();

        // if any titles changed check if aleary existed in db
        if (changedQualificationsTitlesAr.Count > 0 || changedQualificationsTitlesEn.Count > 0)
        {
            var existedQualifications = _crudService.GetList<Qualification, Guid>(e => changedQualificationsTitlesAr.Contains(e.TitleAr) || changedQualificationsTitlesEn.Contains(e.TitleEn));
            // if any existance found in db reject all inputs
            if (existedQualifications.Count > 0)
            {
                _errorMessage = $"Qualifications List was rejected, Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        // do the normal update many items action
        var newQualifications = _mapper.Map<List<Qualification>>(inputs);

        for (int i = 0; i < oldQualifications.Count; i++)
            FillRestPropsWithOldValues(oldQualifications[i], newQualifications[i]);
        var updatedQualifications = _crudService.UpdateAndGetRange<Qualification, Guid>(newQualifications);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(updatedQualifications);
    }

    public bool Delete(Guid id)
    {
        var qualification = GetQualificationById(id);
        _crudService.SoftDelete<Qualification, Guid>(qualification);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var qualification = GetQualificationById(id);
        _crudService.Activate<Qualification, Guid>(qualification);
        _crudService.SaveChanges();
        return true;
    } 

}
