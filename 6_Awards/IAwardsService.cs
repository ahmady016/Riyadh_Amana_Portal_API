using amana_mono._6_Awards.Dtos;
using DB.Common;

namespace amana_mono._6_Awards
{
    public interface IAwardsService
    {
        List<AwardsDto> List(string type);
        PageResult<AwardsDto> ListPage(string type, int pageSize, int pageNumber);
        AwardsDto Find(Guid id);
        List<AwardsDto> FindList(string ids);
        AwardsDto Add(CreateAwardsInput input);
        List<AwardsDto> AddMany(List<CreateAwardsInput> inputs);
        AwardsDto Update(UpdateAwardsInput input);
        List<AwardsDto> UpdateMany(List<UpdateAwardsInput> inputs);
        bool Delete(Guid id);
        bool Activate(Guid id);
    }
}
