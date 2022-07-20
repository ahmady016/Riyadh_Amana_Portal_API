using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Partners;

[ApiController]
[Route("api/[controller]/[action]")]
public class PartnersController : ControllerBase
{
    private readonly IPartnerService _service;
    public PartnersController(IPartnerService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of PartnersDots</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of PartnersDots</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListPage(type, pageSize ?? 10, pageNumber ?? 1));
    }

    /// <summary>
    /// Partner/Find/[id]
    /// </summary>
    /// <returns>PartnersDots</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// Partner/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of PartnersDots</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// Partner/Add
    /// </summary>
    /// <returns>PartnersDots</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreatePartnersInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// Partner/AddMany
    /// </summary>
    /// <returns>List Of PartnersDots</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreatePartnersInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Partner/Update
    /// </summary>
    /// <returns>PartnersDots</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdatePartnersInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// Partner/UpdateMany
    /// </summary>
    /// <returns>List Of PartnersDots</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdatePartnersInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Partner/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// Partner/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }
}
