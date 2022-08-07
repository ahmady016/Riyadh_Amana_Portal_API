using DB.Common;
using Dtos;

namespace Partners;

public interface ILocalPartnerService
{
    List<LocalPartnerDto> List(string type);
    PageResult<LocalPartnerDto> ListPage(string type, int pageSize, int pageNumber);
    LocalPartnerDto Find(Guid id);
    List<LocalPartnerDto> FindList(string ids);
    LocalPartnerDto Add(CreateLocalPartnerInput input);
    List<LocalPartnerDto> AddMany(List<CreateLocalPartnerInput> inputs);
    LocalPartnerDto Update(UpdateLocalPartnerInput input);
    List<LocalPartnerDto> UpdateMany(List<UpdateLocalPartnerInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
