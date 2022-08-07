using Dtos;
using DB.Common;

namespace Lookups;

public interface IQualificationService
{
    List<LookupDto> List(string type);
    PageResult<LookupDto> ListPage(string type, int pageSize, int pageNumber);
    LookupDto FindOne(Guid id);
    List<LookupDto> FindMany(string ids);
    LookupDto Add(CreateLookupInput input);
    List<LookupDto> AddMany(List<CreateLookupInput> inputs);
    LookupDto Update(UpdateLookupInput input);
    List<LookupDto> UpdateMany(List<UpdateLookupInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
