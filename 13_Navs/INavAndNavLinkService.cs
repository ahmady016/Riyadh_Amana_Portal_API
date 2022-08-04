using Dtos;
using DB.Common;

namespace Navs;

public interface INavAndNavLinkService
{
    List<NavWithLinksCountDto> ListNavs(string type);
    PageResult<NavWithLinksCountDto> ListNavsPage(string type, int pageSize, int pageNumber);
    NavDto FindOneNav(Guid id);
    List<NavDto> FindManyNavs(string ids);
    NavDto AddNav(CreateNavInput input);
    List<NavDto> AddManyNavs(List<CreateNavInput> inputs);
    NavDto AddNavWithLinks(CreateNavWithLinksInput input);
    NavDto UpdateNav(UpdateNavInput input);
    List<NavDto> UpdateManyNavs(List<UpdateNavInput> inputs);
    bool DeleteNav(Guid id);
    bool ActivateNav(Guid id);

    List<NavLinkDto> ListNavLinks(string type);
    PageResult<NavLinkDto> ListNavLinksPage(string type, int pageSize, int pageNumber);
    NavLinkDto FindOneNavLink(Guid id);
    List<NavLinkDto> FindManyNavLinks(string ids);
    NavLinkDto AddNavLink(CreateNavLinkWithNavIdInput input);
    List<NavLinkDto> AddManyNavLinks(List<CreateNavLinkInput> inputs);
    NavLinkDto UpdateNavLink(UpdateNavLinkInput input);
    List<NavLinkDto> UpdateManyNavLinks(List<UpdateNavLinkInput> inputs);
    bool DeleteNavLink(Guid id);
    bool ActivateNavLink(Guid id);
}
