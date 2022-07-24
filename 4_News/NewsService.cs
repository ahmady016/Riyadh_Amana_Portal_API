using AutoMapper;
using System.Net;

using DB;
using DB.Entities;
using DB.Common;
using Common;
using Dtos;

namespace _News;

public class NewsService : INewsService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<News> _logger;
    private string _errorMessage;

    public NewsService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<News> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    private News GetById(Guid id)
    {
        var newsItem = _crudService.Find<News, Guid>(id);
        if (newsItem is null)
        {
            _errorMessage = $"News Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return newsItem;
    }
    private List<News> GetByIds(List<Guid> ids)
    {
        var newsList = _crudService.GetList<News, Guid>(e => ids.Contains(e.Id));
        if (newsList.Count == 0)
        {
            _errorMessage = $"No Any News Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return newsList;
    }
    private static void FillRestPropsWithOldValues(News oldItem, News newItem)
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

    public List<NewsDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<News, Guid>(),
            "deleted" => _crudService.GetList<News, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<News, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<NewsDto>>(list);
    }
    public PageResult<NewsDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<News>(),
            "deleted" => _crudService.GetQuery<News>(e => e.IsDeleted),
            _ => _crudService.GetQuery<News>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<NewsDto>()
        {
            PageItems = _mapper.Map<List<NewsDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public NewsDto Find(Guid id)
    {
        var news = GetById(id);
        return _mapper.Map<NewsDto>(news);
    }
    public List<NewsDto> FindList(string ids)
    {
        if (ids is null)
        {
            _errorMessage = $"News: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<NewsDto>>(list);
    }

    public NewsDto Add(CreateNewsInput input)
    {
        var news = _mapper.Map<News>(input);
        var createdNews = _crudService.Add<News, Guid>(news);
        _crudService.SaveChanges();
        return _mapper.Map<NewsDto>(createdNews);
    }
    public List<NewsDto> AddMany(List<CreateNewsInput> inputs)
    {
        var newsList = _mapper.Map<List<News>>(inputs);
        var createdNewsList = _crudService.AddAndGetRange<News, Guid>(newsList);
        _crudService.SaveChanges();
        return _mapper.Map<List<NewsDto>>(createdNewsList);
    }

    public NewsDto Update(UpdateNewsInput input)
    {
        var oldItem = GetById(input.Id);
        var newItem = _mapper.Map<News>(input);

        FillRestPropsWithOldValues(oldItem, newItem);
        var updatedNews = _crudService.Update<News, Guid>(newItem);
        _crudService.SaveChanges();

        return _mapper.Map<NewsDto>(updatedNews);
    }
    public List<NewsDto> UpdateMany(List<UpdateNewsInput> inputs)
    {
        var oldList = GetByIds(inputs.Select(x => x.Id).ToList());
        var newList = _mapper.Map<List<News>>(inputs);

        for (int i = 0; i < oldList.Count; i++)
            FillRestPropsWithOldValues(oldList[i], newList[i]);
        var updatedNewsList = _crudService.UpdateAndGetRange<News, Guid>(newList);
        _crudService.SaveChanges();
        
        return _mapper.Map<List<NewsDto>>(updatedNewsList);
    }

    public bool Delete(Guid id)
    {
        var newsItem = GetById(id);
        _crudService.SoftDelete<News, Guid>(newsItem);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var newsItem = GetById(id);
        _crudService.Activate<News, Guid>(newsItem);
        _crudService.SaveChanges();
        return true;
    }

}
