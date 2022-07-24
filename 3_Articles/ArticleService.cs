using AutoMapper;
using System.Net;

using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Articles;

public class ArticleService : IArticleService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Article> _logger;
    private string _errorMessage;

    public ArticleService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Article> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    private Article GetById(Guid id)
    {
        var article = _crudService.Find<Article, Guid>(id);
        if (article is null)
        {
            _errorMessage = $"Article Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return article;
    }
    private List<Article> GetByIds(List<Guid> ids)
    {
        var articles = _crudService.GetList<Article, Guid>(e => ids.Contains(e.Id));
        if (articles.Count == 0)
        {
            _errorMessage = $"No Any Article Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return articles;
    }
    private static void FillRestPropsWithOldValues(Article oldItem, Article newItem)
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

    public List<ArticleDto> List(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Article, Guid>(),
            "deleted" => _crudService.GetList<Article, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Article, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<ArticleDto>>(list);
    }
    public PageResult<ArticleDto> ListPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Article>(),
            "deleted" => _crudService.GetQuery<Article>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Article>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<ArticleDto>()
        {
            PageItems = _mapper.Map<List<ArticleDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }

    public ArticleDto Find(Guid id)
    {
        var article = GetById(id);
        return _mapper.Map<ArticleDto>(article);
    }
    public List<ArticleDto> FindList(string ids)
    {
        if (ids == null)
        {
            _errorMessage = $"Article: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
        }

        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetByIds(_ids);
        return _mapper.Map<List<ArticleDto>>(list);
    }

    public ArticleDto Add(CreateArticleInput input)
    {
        var article = _mapper.Map<Article>(input);
        var createdArticle = _crudService.Add<Article, Guid>(article);
        _crudService.SaveChanges();
        return _mapper.Map<ArticleDto>(createdArticle);
    }
    public List<ArticleDto> AddMany(List<CreateArticleInput> inputs)
    {
        var articles = _mapper.Map<List<Article>>(inputs);
        var createdArticles = _crudService.AddAndGetRange<Article, Guid>(articles);
        _crudService.SaveChanges();
        return _mapper.Map<List<ArticleDto>>(createdArticles);
    }

    public ArticleDto Update(UpdateArticleInput input)
    {
        var oldArticle = GetById(input.Id);
        var newArticle = _mapper.Map<Article>(input);

        FillRestPropsWithOldValues(oldArticle, newArticle);
        var updatedArticle = _crudService.Update<Article, Guid>(newArticle);
        _crudService.SaveChanges();

        return _mapper.Map<ArticleDto>(updatedArticle);
    }
    public List<ArticleDto> UpdateMany(List<UpdateArticleInput> inputs)
    {
        var oldArticles = GetByIds(inputs.Select(x => x.Id).ToList());
        var newArticles = _mapper.Map<List<Article>>(inputs);

        for (int i = 0; i < oldArticles.Count; i++)
            FillRestPropsWithOldValues(oldArticles[i], newArticles[i]);
        var updatedArticles = _crudService.UpdateAndGetRange<Article, Guid>(newArticles);
        _crudService.SaveChanges();

        return _mapper.Map<List<ArticleDto>>(updatedArticles);
    }

    public bool Delete(Guid id)
    {
        var article = GetById(id);
        _crudService.SoftDelete<Article, Guid>(article);
        _crudService.SaveChanges();
        return true;
    }
    public bool Activate(Guid id)
    {
        var article = GetById(id);
        _crudService.Activate<Article, Guid>(article);
        _crudService.SaveChanges();
        return true;
    }

}
