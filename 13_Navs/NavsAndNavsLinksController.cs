using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Navs;

[ApiController]
[Route("api/[controller]/[action]")]
public class NavsAndNavLinksController : ControllerBase
{
    private readonly INavAndNavLinkService _service;
    public NavsAndNavLinksController(INavAndNavLinkService service)
    {
        _service = service;
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// NavsAndNavLinks/ListNavs/all
    /// </summary>
    /// <returns>List of NavDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListNavs([FromRoute] string type)
    {
        return Ok(_service.ListNavs(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// NavsAndNavLinks/ListNavsPage/all
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
    /// NavsAndNavLinks/FindNav/[id]
    /// </summary>
    /// <returns>NavDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindNav(Guid id)
    {
        return Ok(_service.FindOneNav(id));
    }
    /// <summary>
    /// NavsAndNavLinks/FindNavs/[id, id, id]
    /// </summary>
    /// <returns>List Of NavDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindNavs(string ids)
    {
        return Ok(_service.FindManyNavs(ids));
    }

    /// <summary>
    /// NavsAndNavLinks/AddNav
    /// </summary>
    /// <returns>NavDto</returns>
    [HttpPost]
    public virtual IActionResult AddNav([FromBody] CreateNavInput input)
    {
        return Ok(_service.AddNav(input));
    }
    /// <summary>
    /// NavsAndNavLinks/AddNavWithNavLinks
    /// </summary>
    /// <returns>NavDto</returns>
    [HttpPost]
    public virtual IActionResult AddNavWithNavLinks([FromBody] CreateNavWithLinksInput input)
    {
        return Ok(_service.AddNavWithLinks(input));
    }
    /// <summary>
    /// NavsAndNavLinks/AddManyNavs
    /// </summary>
    /// <returns>List Of NavDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyNavs([FromBody] List<CreateNavInput> inputs)
    {
        return Ok(_service.AddManyNavs(inputs));
    }

    /// <summary>
    /// NavsAndNavLinks/UpdateNav
    /// </summary>
    /// <returns>NavDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateNav([FromBody] UpdateNavInput input)
    {
        return Ok(_service.UpdateNav(input));
    }
    /// <summary>
    /// NavsAndNavLinks/UpdateManyNavs
    /// </summary>
    /// <returns>List Of NavDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyNavs([FromBody] List<UpdateNavInput> inputs)
    {
        return Ok(_service.UpdateManyNavs(inputs));
    }

    /// <summary>
    /// NavsAndNavLinks/DeleteNav
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteNav(Guid id)
    {
        return Ok(_service.DeleteNav(id));
    }
    /// <summary>
    /// NavsAndNavLinks/ActivateNav
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateNav(Guid id)
    {
        return Ok(_service.ActivateNav(id));
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// NavsAndNavLinks/ListNavLinks/all
    /// </summary>
    /// <returns>List of NavLinkDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListNavLinks([FromRoute] string type)
    {
        return Ok(_service.ListNavLinks(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// NavsAndNavLinks/ListNavLinksPage/all
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
    /// NavsAndNavLinks/FindNavLink/[id]
    /// </summary>
    /// <returns>NavLinkDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindNavLink(Guid id)
    {
        return Ok(_service.FindOneNavLink(id));
    }
    /// <summary>
    /// NavsAndNavLinks/FindNavLinks/[id, id, id]
    /// </summary>
    /// <returns>List Of NavLinkDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindNavLinks(string ids)
    {
        return Ok(_service.FindManyNavLinks(ids));
    }

    /// <summary>
    /// NavsAndNavLinks/AddNavLink
    /// </summary>
    /// <returns>NavLinkDto</returns>
    [HttpPost]
    public virtual IActionResult AddNavLink([FromBody] CreateNavLinkInput input)
    {
        return Ok(_service.AddNavLink(input));
    }
    /// <summary>
    /// NavsAndNavLinks/AddManyNavLinks
    /// </summary>
    /// <returns>List Of NavLinkDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyNavLinks([FromBody] List<CreateNavLinkInput> inputs)
    {
        return Ok(_service.AddManyNavLinks(inputs));
    }

    /// <summary>
    /// NavsAndNavLinks/UpdateNavLink
    /// </summary>
    /// <returns>NavLinkDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateNavLink([FromBody] UpdateNavLinkInput input)
    {
        return Ok(_service.UpdateNavLink(input));
    }
    /// <summary>
    /// NavsAndNavLinks/UpdateManyNavLinks
    /// </summary>
    /// <returns>List Of NavLinkDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyNavLinks([FromBody] List<UpdateNavLinkInput> inputs)
    {
        return Ok(_service.UpdateManyNavLinks(inputs));
    }

    /// <summary>
    /// NavsAndNavLinks/DeleteNavLink
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteNavLink(Guid id)
    {
        return Ok(_service.DeleteNavLink(id));
    }
    /// <summary>
    /// NavsAndNavLinks/ActivateNavLink
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateNavLink(Guid id)
    {
        return Ok(_service.ActivateNavLink(id));
    }

}
