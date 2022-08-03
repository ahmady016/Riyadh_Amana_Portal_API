using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Navs;

[ApiController]
[Route("api/[controller]/[action]")]
public class NavsAndNavsLinksController : ControllerBase
{
    private readonly INavAndNavLinkService _service;
    public NavsAndNavsLinksController(INavAndNavLinkService service)
    {
        _service = service;
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// NavsAndNavsLinks/ListNavs/all
    /// </summary>
    /// <returns>List of NavDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListNavs([FromRoute] string type)
    {
        return Ok(_service.ListNavs(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// NavsAndNavsLinks/ListNavsPage/all
    /// </summary>
    /// <returns>List of NavDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListNavsPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListNavsPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// NavsAndNavsLinks/FindNav/[id]
    /// </summary>
    /// <returns>NavDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindNav(Guid id)
    {
        return Ok(_service.FindOneNav(id));
    }
    /// <summary>
    /// NavsAndNavsLinks/FindNavs/[id, id, id]
    /// </summary>
    /// <returns>List Of NavDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindNavs([FromRoute] string ids)
    {
        return Ok(_service.FindManyNavs(ids));
    }

    /// <summary>
    /// NavsAndNavsLinks/AddNav
    /// </summary>
    /// <returns>NavDto</returns>
    [HttpPost]
    public virtual IActionResult AddNav([FromBody] CreateNavInput input)
    {
        return Ok(_service.AddNav(input));
    }
    /// <summary>
    /// NavsAndNavsLinks/AddNavWithLinks
    /// </summary>
    /// <returns>NavDto</returns>
    [HttpPost]
    public virtual IActionResult AddNavWithLinks([FromBody] CreateNavWithLinksInput input)
    {
        return Ok(_service.AddNavWithLinks(input));
    }
    /// <summary>
    /// NavsAndNavsLinks/AddManyNavs
    /// </summary>
    /// <returns>List Of NavDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyNavs([FromBody] List<CreateNavInput> inputs)
    {
        return Ok(_service.AddManyNavs(inputs));
    }

    /// <summary>
    /// NavsAndNavsLinks/UpdateNav
    /// </summary>
    /// <returns>NavDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateNav([FromBody] UpdateNavInput input)
    {
        return Ok(_service.UpdateNav(input));
    }
    /// <summary>
    /// NavsAndNavsLinks/UpdateManyNavs
    /// </summary>
    /// <returns>List Of NavDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyNavs([FromBody] List<UpdateNavInput> inputs)
    {
        return Ok(_service.UpdateManyNavs(inputs));
    }

    /// <summary>
    /// NavsAndNavsLinks/DeleteNav
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteNav(Guid id)
    {
        return Ok(_service.DeleteNav(id));
    }
    /// <summary>
    /// NavsAndNavsLinks/ActivateNav
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateNav(Guid id)
    {
        return Ok(_service.ActivateNav(id));
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// NavsAndNavsLinks/ListNavLinks/all
    /// </summary>
    /// <returns>List of NavLinkDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListNavLinks([FromRoute] string type)
    {
        return Ok(_service.ListNavLinks(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// NavsAndNavsLinks/ListNavLinksPage/all
    /// </summary>
    /// <returns>List of NavLinkDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListNavLinksPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListNavLinksPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// NavsAndNavsLinks/FindNavLink/[id]
    /// </summary>
    /// <returns>NavLinkDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindNavLink(Guid id)
    {
        return Ok(_service.FindOneNavLink(id));
    }
    /// <summary>
    /// NavsAndNavsLinks/FindNavLinks/[id, id, id]
    /// </summary>
    /// <returns>List Of NavLinkDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindNavLinks(string ids)
    {
        return Ok(_service.FindManyNavLinks(ids));
    }

    /// <summary>
    /// NavsAndNavsLinks/AddNavLink
    /// </summary>
    /// <returns>NavLinkDto</returns>
    [HttpPost]
    public virtual IActionResult AddNavLink([FromBody] CreateNavLinkWithNavIdInput input)
    {
        return Ok(_service.AddNavLink(input));
    }
    /// <summary>
    /// NavsAndNavsLinks/AddManyNavLinks
    /// </summary>
    /// <returns>List Of NavLinkDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyNavLinks([FromBody] List<CreateNavLinkInput> inputs)
    {
        return Ok(_service.AddManyNavLinks(inputs));
    }

    /// <summary>
    /// NavsAndNavsLinks/UpdateNavLink
    /// </summary>
    /// <returns>NavLinkDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateNavLink([FromBody] UpdateNavLinkInput input)
    {
        return Ok(_service.UpdateNavLink(input));
    }
    /// <summary>
    /// NavsAndNavsLinks/UpdateManyNavLinks
    /// </summary>
    /// <returns>List Of NavLinkDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyNavLinks([FromBody] List<UpdateNavLinkInput> inputs)
    {
        return Ok(_service.UpdateManyNavLinks(inputs));
    }

    /// <summary>
    /// NavsAndNavsLinks/DeleteNavLink
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteNavLink(Guid id)
    {
        return Ok(_service.DeleteNavLink(id));
    }
    /// <summary>
    /// NavsAndNavsLinks/ActivateNavLink
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateNavLink(Guid id)
    {
        return Ok(_service.ActivateNavLink(id));
    }

}
