using System.Net;
using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace AppPages;

public class AppPageAndPageKeyService : IAppPageAndPageKeyService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<AppPage> _logger;
    private string _errorMessage;

    public AppPageAndPageKeyService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<AppPage> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    #region private Methods
    private AppPage GetAppPageById(Guid id)
    {
        var appPage = _crudService.Find<AppPage, Guid>(id);
        if (appPage is null)
        {
            _errorMessage = $"AppPage Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return appPage;
    }
    private List<AppPage> GetAppPagesByIds(List<Guid> ids)
    {
        var appPages = _crudService.GetList<AppPage, Guid>(e => ids.Contains(e.Id));
        if (appPages.Count == 0)
        {
            _errorMessage = $"No Any AppPage Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return appPages;
    }
    private static void FillRestPropsWithOldValues(AppPage oldItem, AppPage newItem)
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

    private PageKey GetPageKeyById(Guid id)
    {
        var pageKey = _crudService.Find<PageKey, Guid>(id);
        if (pageKey is null)
        {
            _errorMessage = $"PageKey Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return pageKey;
    }
    private List<PageKey> GetPageKeysByIds(List<Guid> ids)
    {
        var pageKeys = _crudService.GetList<PageKey, Guid>(e => ids.Contains(e.Id));
        if (pageKeys.Count == 0)
        {
            _errorMessage = $"No Any PageKey Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return pageKeys;
    }
    private static void FillRestPropsWithOldValues(PageKey oldItem, PageKey newItem)
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

    public List<AppPageDto> ListAppPages(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<AppPage, Guid>(),
            "deleted" => _crudService.GetList<AppPage, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<AppPage, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<AppPageDto>>(list);
    }
    public PageResult<AppPageDto> ListAppPagesPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<AppPage>(),
            "deleted" => _crudService.GetQuery<AppPage>(e => e.IsDeleted),
            _ => _crudService.GetQuery<AppPage>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<AppPageDto>()
        {
            PageItems = _mapper.Map<List<AppPageDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public AppPageDto FindOneAppPage(Guid id)
    {
        var appPage = GetAppPageById(id);
        return _mapper.Map<AppPageDto>(appPage);
    }
    public List<AppPageDto> FindManyAppPages(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"AppPage: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetAppPagesByIds(_ids);
        return _mapper.Map<List<AppPageDto>>(list);
    }

    public AppPageDto AddAppPage(CreateAppPageInput input)
    {
        var appPage = _mapper.Map<AppPage>(input);
        var createdAppPage = _crudService.Add<AppPage, Guid>(appPage);
        _crudService.SaveChanges();
        return _mapper.Map<AppPageDto>(createdAppPage);
    }
    public AppPageDto AddAppPageWithKeys(CreateAppPageWithKeysInput input)
    {
        var appPage = _mapper.Map<AppPage>(input);
        var createdAppPage = _crudService.Add<AppPage, Guid>(appPage);
        _crudService.SaveChanges();
        return _mapper.Map<AppPageDto>(createdAppPage);
    }
    public List<AppPageDto> AddManyAppPages(List<CreateAppPageInput> inputs)
    {
        var appPages = _mapper.Map<List<AppPage>>(inputs);
        var createdAppPages = _crudService.AddAndGetRange<AppPage, Guid>(appPages);
        _crudService.SaveChanges();
        return _mapper.Map<List<AppPageDto>>(createdAppPages);
    }

    public AppPageDto UpdateAppPage(UpdateAppPageInput input)
    {
        var oldAppPage = GetAppPageById(input.Id);
        var newAppPage = _mapper.Map<AppPage>(input);

        FillRestPropsWithOldValues(oldAppPage, newAppPage);
        var updatedAppPage = _crudService.Update<AppPage, Guid>(newAppPage);
        _crudService.SaveChanges();

        return _mapper.Map<AppPageDto>(updatedAppPage);
    }
    public List<AppPageDto> UpdateManyAppPages(List<UpdateAppPageInput> inputs)
    {
        var oldAppPages = GetAppPagesByIds(inputs.Select(x => x.Id).ToList());
        var newAppPages = _mapper.Map<List<AppPage>>(inputs);

        for (int i = 0; i < oldAppPages.Count; i++)
            FillRestPropsWithOldValues(oldAppPages[i], newAppPages[i]);
        var updatedAppPages = _crudService.UpdateAndGetRange<AppPage, Guid>(newAppPages);
        _crudService.SaveChanges();

        return _mapper.Map<List<AppPageDto>>(updatedAppPages);
    }

    public bool DeleteAppPage(Guid id)
    {
        var appPage = GetAppPageById(id);
        _crudService.SoftDelete<AppPage, Guid>(appPage);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateAppPage(Guid id)
    {
        var appPage = GetAppPageById(id);
        _crudService.Activate<AppPage, Guid>(appPage);
        _crudService.SaveChanges();
        return true;
    }

    public List<PageKeyDto> ListPageKeys(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<PageKey, Guid>(),
            "deleted" => _crudService.GetList<PageKey, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<PageKey, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<PageKeyDto>>(list);
    }
    public PageResult<PageKeyDto> ListPageKeysPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<PageKey>(),
            "deleted" => _crudService.GetQuery<PageKey>(e => e.IsDeleted),
            _ => _crudService.GetQuery<PageKey>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<PageKeyDto>()
        {
            PageItems = _mapper.Map<List<PageKeyDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public PageKeyDto FindOnePageKey(Guid id)
    {
        var pageKey = GetPageKeyById(id);
        return _mapper.Map<PageKeyDto>(pageKey);
    }
    public List<PageKeyDto> FindManyPageKeys(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"PageKey: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetPageKeysByIds(_ids);
        return _mapper.Map<List<PageKeyDto>>(list);
    }

    public PageKeyDto AddPageKey(CreatePageKeyInput input)
    {
        var pageKey = _mapper.Map<PageKey>(input);
        var createdPageKey = _crudService.Add<PageKey, Guid>(pageKey);
        _crudService.SaveChanges();
        return _mapper.Map<PageKeyDto>(createdPageKey);
    }
    public List<PageKeyDto> AddManyPageKeys(List<CreatePageKeyInput> inputs)
    {
        var PageKeys = _mapper.Map<List<PageKey>>(inputs);
        var createdPageKeys = _crudService.AddAndGetRange<PageKey, Guid>(PageKeys);
        _crudService.SaveChanges();
        return _mapper.Map<List<PageKeyDto>>(createdPageKeys);
    }

    public PageKeyDto UpdatePageKey(UpdatePageKeyInput input)
    {
        var oldPageKey = GetPageKeyById(input.Id);
        var newPageKey = _mapper.Map<PageKey>(input);

        FillRestPropsWithOldValues(oldPageKey, newPageKey);
        var updatedPageKey = _crudService.Update<PageKey, Guid>(newPageKey);
        _crudService.SaveChanges();

        return _mapper.Map<PageKeyDto>(updatedPageKey);
    }
    public List<PageKeyDto> UpdateManyPageKeys(List<UpdatePageKeyInput> inputs)
    {
        var oldPageKeys = GetPageKeysByIds(inputs.Select(x => x.Id).ToList());
        var newPageKeys = _mapper.Map<List<PageKey>>(inputs);

        for (int i = 0; i < oldPageKeys.Count; i++)
            FillRestPropsWithOldValues(oldPageKeys[i], newPageKeys[i]);
        var updatedPageKeys = _crudService.UpdateAndGetRange<PageKey, Guid>(newPageKeys);
        _crudService.SaveChanges();

        return _mapper.Map<List<PageKeyDto>>(updatedPageKeys);
    }
    
    public bool DeletePageKey(Guid id)
    {
        var pageKey = GetPageKeyById(id);
        _crudService.SoftDelete<PageKey, Guid>(pageKey);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivatePageKey(Guid id)
    {
        var pageKey = GetPageKeyById(id);
        _crudService.Activate<PageKey, Guid>(pageKey);
        _crudService.SaveChanges();
        return true;
    }

}
