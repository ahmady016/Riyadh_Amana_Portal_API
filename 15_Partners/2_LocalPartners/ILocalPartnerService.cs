using DB.Common;
using Dtos;

namespace Partners;

public interface IPartnerService
{
    List<PartnerDto> List(string type);
    PageResult<PartnerDto> ListPage(string type, int pageSize, int pageNumber);
    PartnerDto Find(Guid id);
    List<PartnerDto> FindList(string ids);
    PartnerDto Add(CreatePartnerInput input);
    List<PartnerDto> AddMany(List<CreatePartnerInput> inputs);
    PartnerDto Update(UpdatePartnerInput input);
    List<PartnerDto> UpdateMany(List<UpdatePartnerInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
