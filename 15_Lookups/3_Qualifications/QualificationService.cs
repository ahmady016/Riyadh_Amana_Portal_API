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
            _errorMessage = $"No Any Qualification Records Found";
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

    public List<LookupDto> ListQualifications(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Qualification, Guid>(),
            "deleted" => _crudService.GetList<Qualification, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Qualification, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<LookupDto>>(list);
    }
    public PageResult<LookupDto> ListQualificationsPage(string type, int pageSize, int pageNumber)
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
    public LookupDto FindOneQualification(Guid id)
    {
        var qualification = GetQualificationById(id);
        return _mapper.Map<LookupDto>(qualification);
    }
    public List<LookupDto> FindManyQualifications(string ids)
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

    public LookupDto AddQualification(CreateLookupInput input)
    {
        var oldQualification = _crudService.GetOne<Qualification>(e=> e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        if (oldQualification is not null)
        {
            _errorMessage = $"Qualification: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var qualification = _mapper.Map<Qualification>(input);
        var createdQualification = _crudService.Add<Qualification, Guid>(qualification);
        _crudService.SaveChanges();
        return _mapper.Map<LookupDto>(createdQualification);
    }
    public List<LookupDto> AddManyQualifications(List<CreateLookupInput> inputs)
    {
        var titleArList = inputs.Select(e=>e.TitleAr).ToList();
        var titleEnList = inputs.Select(e=>e.TitleEn).ToList();
        var qualificationsExisted = _crudService.GetList<Qualification, Guid>(e=> titleArList.Contains(e.TitleAr) || titleEnList.Contains(e.TitleEn));
        if (qualificationsExisted.Count != 0)
        {
            _errorMessage = $"Qualification: Qualifications List Is rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var qualifications = _mapper.Map<List<Qualification>>(inputs);
        var createdQualifications = _crudService.AddAndGetRange<Qualification, Guid>(qualifications);
        _crudService.SaveChanges();
        return _mapper.Map<List<LookupDto>>(createdQualifications);
    }

    public LookupDto UpdateQualification(UpdateLookupInput input)
    {
        var oldQualification = GetQualificationById(input.Id);
        if (oldQualification.TitleAr != input.TitleAr || oldQualification.TitleEn != input.TitleEn ) {
            var qualificationExisted = _crudService.GetOne<Qualification>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            if (qualificationExisted is not null) {
                _errorMessage = $"Qualification: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        var newQualification = _mapper.Map<Qualification>(input);
        FillRestPropsWithOldValues(oldQualification, newQualification);
        var updatedQualification = _crudService.Update<Qualification, Guid>(newQualification);
        _crudService.SaveChanges();

        return _mapper.Map<LookupDto>(updatedQualification);
    }
    public List<LookupDto> UpdateManyQualifications(List<UpdateLookupInput> inputs)
    {
        var oldQualifications = GetQualificationsByIds(inputs.Select(x => x.Id).ToList());

        var oldQualificationsTitlesAr = oldQualifications.Where(m => !inputs.Select(e => e.TitleAr).Contains(m.TitleAr)).Select(e => e.TitleAr).ToList();
        var oldQualificationsTitleEn = oldQualifications.Where(m => !inputs.Select(e => e.TitleEn).Contains(m.TitleEn)).Select(e => e.TitleEn).ToList();
        if (oldQualificationsTitlesAr.Count != 0 || oldQualificationsTitleEn.Count != 0)
        {
            var qualificationsExisted = _crudService.GetList<Qualification, Guid>(e => oldQualificationsTitlesAr.Contains(e.TitleAr) || oldQualificationsTitleEn.Contains(e.TitleEn));
            if (qualificationsExisted.Count != 0)
            {
                _errorMessage = $"Qualification: Qualifications List Is rejected , Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }


        var newQualifications = _mapper.Map<List<Qualification>>(inputs);

        for (int i = 0; i < oldQualifications.Count; i++)
            FillRestPropsWithOldValues(oldQualifications[i], newQualifications[i]);
        var updatedQualifications = _crudService.UpdateAndGetRange<Qualification, Guid>(newQualifications);
        _crudService.SaveChanges();

        return _mapper.Map<List<LookupDto>>(updatedQualifications);
    }

    public bool DeleteQualification(Guid id)
    {
        var qualification = GetQualificationById(id);
        _crudService.SoftDelete<Qualification, Guid>(qualification);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateQualification(Guid id)
    {
        var qualification = GetQualificationById(id);
        _crudService.Activate<Qualification, Guid>(qualification);
        _crudService.SaveChanges();
        return true;
    } 
}
