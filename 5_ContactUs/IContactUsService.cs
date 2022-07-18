using DB.Common;
using Dtos;

namespace _ContactUs;

public interface IContactUsService
{
    List<ContactUsDto> List(string type);
    PageResult<ContactUsDto> ListPage(string type, int pageSize, int pageNumber);
    ContactUsDto Find(Guid id);
    List<ContactUsDto> FindList(string ids);
    ContactUsDto Add(CreateContactUsInput input);
    List<ContactUsDto> AddMany(List<CreateContactUsInput> inputs);
    ContactUsDto Update(UpdateContactUsInput input);
    List<ContactUsDto> UpdateMany(List<UpdateContactUsInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
