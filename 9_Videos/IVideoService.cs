using DB.Common;
using Dtos;

namespace Videos;

public interface IVideoService
{
    List<VideoDto> List(string type);
    PageResult<VideoDto> ListPage(string type, int pageSize, int pageNumber);
    VideoDto Find(Guid id);
    List<VideoDto> FindList(string ids);
    VideoDto Add(CreateVideoInput input);
    List<VideoDto> AddMany(List<CreateVideoInput> inputs);
    VideoDto Update(UpdateVideoInput input);
    List<VideoDto> UpdateMany(List<UpdateVideoInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
