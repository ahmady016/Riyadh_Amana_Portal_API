using Dtos;
using DB.Common;

namespace Lookups;

public interface IQualificationService
{
    List<LookupDto> ListQualifications(string type);
    PageResult<LookupDto> ListQualificationsPage(string type, int pageSize, int pageNumber);
    LookupDto FindOneQualification(Guid id);
    List<LookupDto> FindManyQualifications(string ids);
    LookupDto AddQualification(CreateLookupInput input);
    List<LookupDto> AddManyQualifications(List<CreateLookupInput> inputs);
    LookupDto UpdateQualification(UpdateLookupInput input);
    List<LookupDto> UpdateManyQualifications(List<UpdateLookupInput> inputs);
    bool DeleteQualification(Guid id);
    bool ActivateQualification(Guid id);

}
