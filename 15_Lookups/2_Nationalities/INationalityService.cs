using Dtos;
using DB.Common;

namespace Lookups;

public interface INationalityService
{ 
    List<LookupDto> ListNationalities(string type);
    PageResult<LookupDto> ListNationalitiesPage(string type, int pageSize, int pageNumber);
    LookupDto FindOneNationality(Guid id);
    List<LookupDto> FindManyNationalities(string ids);
    LookupDto AddNationality(CreateLookupInput input);
    List<LookupDto> AddManyNationalities(List<CreateLookupInput> inputs);
    LookupDto UpdateNationality(UpdateLookupInput input);
    List<LookupDto> UpdateManyNationalities(List<UpdateLookupInput> inputs);
    bool DeleteNationality(Guid id);
    bool ActivateNationality(Guid id);
}
