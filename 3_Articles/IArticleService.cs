using DB.Common;
using Dtos;

namespace Articles;

public interface IArticleService
{
    List<ArticleDto> List(string type);
    PageResult<ArticleDto> ListPage(string type, int pageSize, int pageNumber);
    ArticleDto Find(Guid id);
    List<ArticleDto> FindList(string ids);
    ArticleDto Add(CreateArticleInput input);
    List<ArticleDto> AddMany(List<CreateArticleInput> inputs);
    ArticleDto Update(UpdateArticleInput input);
    List<ArticleDto> UpdateMany(List<UpdateArticleInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
