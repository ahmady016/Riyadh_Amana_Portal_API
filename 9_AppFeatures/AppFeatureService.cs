using AutoMapper;
using System.Net;

using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace AppFeatures;

public class AppFeatureService : IAppFeatureService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<AppFeature> _logger;
    private string _errorMessage;

    public AppFeatureService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<AppFeature> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    private AppFeature GetById(Guid id)
    {
        var article = _crudService.Find<AppFeature, Guid>(id);
        if (article is null)
        {
            _errorMessage = $"AppFeature Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return article;
    }
    private List<AppFeature> GetByIds(List<Guid> ids)
    {
        var articles = _crudService.GetList<AppFeature, Guid>(e => ids.Contains(e.Id));
        if (articles.Count == 0)
        {
            _errorMessage = $"No Any AppFeature Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return articles;
    }
    private static void FillRestPropsWithOldValues(AppFeature oldItem, AppFeature newItem)
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

    public List<AppFeatureDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<AppFeature, Guid>(),
            "deleted" => _crudService.GetList<AppFeature, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<AppFeature, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<AppFeatureDto>>(list);
    }
    public PageResult<AppFeatureDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<AppFeature>(),
            "deleted" => _crudService.GetQuery<AppFeature>(e => e.IsDeleted),
            _ => _crudService.GetQuery<AppFeature>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<AppFeatureDto>()
        {
            PageItems = _mapper.Map<List<AppFeatureDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public AppFeatureDto Find(Guid id)
    {
        var article = GetById(id);
        return _mapper.Map<AppFeatureDto>(article);
    }
    public List<AppFeatureDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"AppFeature: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }

        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<AppFeatureDto>>(list);
    }

    public AppFeatureDto Add(CreateAppFeatureInput input)
    {
        var appFeature = _mapper.Map<AppFeature>(input);
        var createdAppFeature = _crudService.Add<AppFeature, Guid>(appFeature);
        _crudService.SaveChanges();
        return _mapper.Map<AppFeatureDto>(createdAppFeature);
    }
    public List<AppFeatureDto> AddMany(List<CreateAppFeatureInput> inputs)
    {
        var appFeatures = _mapper.Map<List<AppFeature>>(inputs);
        var createdAppFeatures = _crudService.AddAndGetRange<AppFeature, Guid>(appFeatures);
        _crudService.SaveChanges();
        return _mapper.Map<List<AppFeatureDto>>(createdAppFeatures);
    }

    public AppFeatureDto Update(UpdateAppFeatureInput input)
    {
        var oldFeature = GetById(input.Id);
        var newFeature = _mapper.Map<AppFeature>(input);

        FillRestPropsWithOldValues(oldFeature, newFeature);
        var updatedFeature = _crudService.Update<AppFeature, Guid>(newFeature);
        _crudService.SaveChanges();

        return _mapper.Map<AppFeatureDto>(updatedFeature);
    }
    public List<AppFeatureDto> UpdateMany(List<UpdateAppFeatureInput> inputs)
    {
        var oldFeatures = GetByIds(inputs.Select(x => x.Id).ToList());
        var newFeatures = _mapper.Map<List<AppFeature>>(inputs);

        for (int i = 0; i < oldFeatures.Count; i++)
            FillRestPropsWithOldValues(oldFeatures[i], newFeatures[i]);
        var updatedArticles = _crudService.UpdateAndGetRange<AppFeature, Guid>(newFeatures);
        _crudService.SaveChanges();

        return _mapper.Map<List<AppFeatureDto>>(updatedArticles);
    }

    public bool Delete(Guid id)
    {
        var appFeature = GetById(id);
        _crudService.SoftDelete<AppFeature, Guid>(appFeature);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var appFeature = GetById(id);
        _crudService.Activate<AppFeature, Guid>(appFeature);
        _crudService.SaveChanges();
        return true;
    }

}
