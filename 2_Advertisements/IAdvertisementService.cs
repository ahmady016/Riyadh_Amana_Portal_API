using DB.Common;
using Dtos;

namespace Advertisements;

public interface IAdvertisementService
{
    List<AdvertisementDto> List(string type);
    PageResult<AdvertisementDto> ListPage(string type, int pageSize, int pageNumber);
    AdvertisementDto Find(Guid id);
    List<AdvertisementDto> FindList(string ids);
    AdvertisementDto Add(CreateAdvertisementInput input);
    List<AdvertisementDto> AddMany(List<CreateAdvertisementInput> inputs);
    AdvertisementDto Update(UpdateAdvertisementInput input);
    List<AdvertisementDto> UpdateMany(List<UpdateAdvertisementInput> inputs);
    bool Delete(Guid id);
    bool Activate(Guid id);
}
