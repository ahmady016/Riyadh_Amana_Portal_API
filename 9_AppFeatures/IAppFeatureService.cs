using DB.Common;
using Dtos;

namespace AppFeatures;

public interface IAppFeatureService
{
    List<AppFeatureDto> List(string type);
    PageResult<AppFeatureDto> ListPage(string type, int pageSize, int pageNumber);
    AppFeatureDto Find(Guid id);
    List<AppFeatureDto> FindList(string ids);
    AppFeatureDto Add(CreateAppFeatureInput input);
    List<AppFeatureDto> AddMany(List<CreateAppFeatureInput> inputs);
    AppFeatureDto Update(UpdateAppFeatureInput input);
    List<AppFeatureDto> UpdateMany(List<UpdateAppFeatureInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
