using DB.Common;
using Dtos;

namespace Partners;

public interface IPartnersService
{
    List<PartnersDots> List(string type);
    PageResult<PartnersDots> ListPage(string type, int pageSize, int pageNumber);
    PartnersDots Find(Guid id);
    List<PartnersDots> FindList(string ids);
    PartnersDots Add(CreatePartnersInput input);
    List<PartnersDots> AddMany(List<CreatePartnersInput> inputs);
    PartnersDots Update(UpdatePartnersInput input);
    List<PartnersDots> UpdateMany(List<UpdatePartnersInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
