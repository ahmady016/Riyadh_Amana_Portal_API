using Dtos;
using DB.Common;

namespace Lookups;

public interface ICityService
{
    List<LookupDto> ListCities(string type);
    PageResult<LookupDto> ListCitiesPage(string type, int pageSize, int pageNumber);
    LookupDto FindOneCity(Guid id);
    List<LookupDto> FindManyCities(string ids);
    LookupDto AddCity(CreateLookupInput input);
    List<LookupDto> AddManyCities(List<CreateLookupInput> inputs);
    LookupDto UpdateCity(UpdateLookupInput input);
    List<LookupDto> UpdateManyCities(List<UpdateLookupInput> inputs);
    bool DeleteCity(Guid id);
    bool ActivateCity(Guid id);
}
