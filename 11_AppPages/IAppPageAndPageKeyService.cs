using Dtos;
using DB.Common;

namespace AppPages;

public interface IAppPageAndPageKeyService
{
    List<AppPageDto> ListAppPages(string type);
    PageResult<AppPageDto> ListAppPagesPage(string type, int pageSize, int pageNumber);
    AppPageDto FindOneAppPage(Guid id);
    List<AppPageDto> FindManyAppPages(string ids);
    AppPageDto AddAppPage(CreateAppPageInput input);
    List<AppPageDto> AddManyAppPages(List<CreateAppPageInput> inputs);
    AppPageDto AddAppPageWithKeys(CreateAppPageWithKeysInput input);
    AppPageDto UpdateAppPage(UpdateAppPageInput input);
    List<AppPageDto> UpdateManyAppPages(List<UpdateAppPageInput> inputs);
    bool DeleteAppPage(Guid id);
    bool ActivateAppPage(Guid id);

    List<PageKeyDto> ListPageKeys(string type);
    PageResult<PageKeyDto> ListPageKeysPage(string type, int pageSize, int pageNumber);
    PageKeyDto FindOnePageKey(Guid id);
    List<PageKeyDto> FindManyPageKeys(string ids);
    PageKeyDto AddPageKey(CreatePageKeyInput input);
    List<PageKeyDto> AddManyPageKeys(List<CreatePageKeyInput> inputs);
    PageKeyDto UpdatePageKey(UpdatePageKeyInput input);
    List<PageKeyDto> UpdateManyPageKeys(List<UpdatePageKeyInput> inputs);
    bool DeletePageKey(Guid id);
    bool ActivatePageKey(Guid id);
}
