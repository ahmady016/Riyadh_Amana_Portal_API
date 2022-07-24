using AutoMapper;
using System.Net;

using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Awards;

public class AwardService: IAwardService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Award> _logger;
    private string _errorMessage;

    public AwardService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Award> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    private Award GetById(Guid id)
    {
        var award = _crudService.Find<Award, Guid>(id);
        if (award is null)
        {
            _errorMessage = $"Award Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return award;
    }
    private List<Award> GetByIds(List<Guid> ids)
    {
        var awards = _crudService.GetList<Award, Guid>(e => ids.Contains(e.Id));
        if (awards.Count == 0)
        {
            _errorMessage = $"No Any Award Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return awards;
    }
    private static void FillRestPropsWithOldValues(Award oldItem, Award newItem)
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

    public List<AwardDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Award, Guid>(),
            "deleted" => _crudService.GetList<Award, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Award, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<AwardDto>>(list);
    }
    public PageResult<AwardDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Award>(),
            "deleted" => _crudService.GetQuery<Award>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Award>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<AwardDto>()
        {
            PageItems = _mapper.Map<List<AwardDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public AwardDto Find(Guid id)
    {
        var user = GetById(id);
        return _mapper.Map<AwardDto>(user);
    }
    public List<AwardDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Award: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<AwardDto>>(list);
    }

    public AwardDto Add(CreateAwardInput input)
    {
        var award = _mapper.Map<Award>(input);
        var createdaward = _crudService.Add<Award, Guid>(award);
        _crudService.SaveChanges();
        return _mapper.Map<AwardDto>(createdaward);
    }
    public List<AwardDto> AddMany(List<CreateAwardInput> inputs)
    {
        var awardList = _mapper.Map<List<Award>>(inputs);
        var createdAwardList = _crudService.AddAndGetRange<Award, Guid>(awardList);
        _crudService.SaveChanges();
        return _mapper.Map<List<AwardDto>>(createdAwardList);
    }

    public AwardDto Update(UpdateAwardInput input)
    {
        var oldAward = GetById(input.Id);
        var newAward = _mapper.Map<Award>(input);

        FillRestPropsWithOldValues(oldAward, newAward);
        var updatedAward = _crudService.Update<Award, Guid>(newAward);
        _crudService.SaveChanges();
        
        return _mapper.Map<AwardDto>(updatedAward);
    }
    public List<AwardDto> UpdateMany(List<UpdateAwardInput> inputs)
    {
        var oldAwardList = GetByIds(inputs.Select(x => x.Id).ToList());
        var newAwardList = _mapper.Map<List<Award>>(inputs);

        for (int i = 0; i < oldAwardList.Count; i++)
            FillRestPropsWithOldValues(oldAwardList[i], newAwardList[i]);
        var updatedAwardList = _crudService.UpdateAndGetRange<Award, Guid>(newAwardList);
        _crudService.SaveChanges();
        
        return _mapper.Map<List<AwardDto>>(updatedAwardList);
    }

    public bool Delete(Guid id)
    {
        var award = GetById(id);
        _crudService.SoftDelete<Award, Guid>(award);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var award = GetById(id);
        _crudService.Activate<Award, Guid>(award);
        _crudService.SaveChanges();
        return true;
    }

}
