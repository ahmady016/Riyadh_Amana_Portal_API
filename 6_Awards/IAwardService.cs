using DB.Common;
using Dtos;

namespace Awards;

public interface IAwardService
{
    List<AwardDto> List(string type);
    PageResult<AwardDto> ListPage(string type, int pageSize, int pageNumber);
    AwardDto Find(Guid id);
    List<AwardDto> FindList(string ids);
    AwardDto Add(CreateAwardInput input);
    List<AwardDto> AddMany(List<CreateAwardInput> inputs);
    AwardDto Update(UpdateAwardInput input);
    List<AwardDto> UpdateMany(List<UpdateAwardInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
