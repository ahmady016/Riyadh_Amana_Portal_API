using DB.Common;
using Dtos;

namespace Partners;

public interface INormalPartnerService
{
    List<NormalPartnerDto> List(string type);
    PageResult<NormalPartnerDto> ListPage(string type, int pageSize, int pageNumber);
    NormalPartnerDto Find(Guid id);
    List<NormalPartnerDto> FindList(string ids);
    NormalPartnerDto Add(CreateNormalPartnerInput input);
    List<NormalPartnerDto> AddMany(List<CreateNormalPartnerInput> inputs);
    NormalPartnerDto Update(UpdateNormalPartnerInput input);
    List<NormalPartnerDto> UpdateMany(List<UpdateNormalPartnerInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
