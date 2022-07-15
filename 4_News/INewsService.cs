using DB.Common;
using Dtos;

namespace _News;

public interface INewsService
{
    List<NewsDto> List(string type);
    PageResult<NewsDto> ListPage(string type, int pageSize, int pageNumber);
    NewsDto Find(Guid id);
    List<NewsDto> FindList(string ids);
    NewsDto Add(CreateNewsInput input);
    List<NewsDto> AddMany(List<CreateNewsInput> inputs);
    NewsDto Update(UpdateNewsInput input);
    List<NewsDto> UpdateMany(List<UpdateNewsInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
