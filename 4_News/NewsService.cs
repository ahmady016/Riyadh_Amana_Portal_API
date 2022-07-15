using AutoMapper;
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
        var news = _crudService.Find<News, Guid>(id);
        if (news == null)
        {
            _errorMessage = $"News Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }
        return _mapper.Map<NewsDto>(news);
    }
    public List<NewsDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"News: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }

        var _ids = ids.SplitAndRemoveEmpty(',').ToList();
        var list = _crudService.GetList<News, Guid>(e => _ids.Contains(e.Id.ToString()));
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
        var news = _mapper.Map<News>(input);
        var updatedNews = _crudService.Update<News, Guid>(news);
        _crudService.SaveChanges();
        return _mapper.Map<NewsDto>(updatedNews);
    }
    public List<NewsDto> UpdateMany(List<UpdateNewsInput> inputs)
    {
        var newsList = _mapper.Map<List<News>>(inputs);
        var updatedNewsList = _crudService.UpdateAndGetRange<News, Guid>(newsList);
        _crudService.SaveChanges();
        return _mapper.Map<List<NewsDto>>(updatedNewsList);
    }

    public bool Delete(Guid id)
    {
        var news = _crudService.Find<News, Guid>(id);
        if (news is not null)
        {
            _crudService.SoftDelete<News, Guid>(news);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"News record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }
    public bool Activate(Guid id)
    {
        var news = _crudService.Find<News, Guid>(id);
        if (news is not null)
        {
            _crudService.Activate<News, Guid>(news);
            _crudService.SaveChanges();
            return true;
        }
        _errorMessage = $"News record Not Found!!!";
        _logger.LogError(_errorMessage);
        throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
    }

}
