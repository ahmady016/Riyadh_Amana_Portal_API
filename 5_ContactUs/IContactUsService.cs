using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amana_mono._5_ContactUs
{
    public class IContactUsService
    {
        List<ContactUsDto> List(string type);
        PageResult<ContactUsDto> ListPage(string type, int pageSize, int pageNumber);
        ContactUsDto Find(Guid id);
        List<ContactUsDto> FindList(string ids);
        ContactUsDto Add(CreateNewsInput input);
        List<ContactUsDto> AddMany(List<CreateNewsInput> inputs);
        ContactUsDto Update(UpdateNewsInput input);
        List<ContactUsDto> UpdateMany(List<UpdateNewsInput> inputs);
        bool Delete(Guid id);
        bool Activate(Guid id);
    }
}
