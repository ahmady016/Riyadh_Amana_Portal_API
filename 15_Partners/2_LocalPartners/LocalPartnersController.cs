using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Partners;

[ApiController]
[Route("api/[controller]/[action]")]
public class LocalPartnersController : ControllerBase
{
    private readonly ILocalPartnerService _service;
    public LocalPartnersController(ILocalPartnerService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of LocalPartnerDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of LocalPartnerDto</returns>
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
    /// LocalPartners/Find/[id]
    /// </summary>
    /// <returns>LocalPartnerDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// LocalPartners/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of LocalPartnerDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// LocalPartners/Add
    /// </summary>
    /// <returns>LocalPartnerDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateLocalPartnerInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// LocalPartners/AddMany
    /// </summary>
    /// <returns>List Of LocalPartnerDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateLocalPartnerInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// LocalPartners/Update
    /// </summary>
    /// <returns>LocalPartnerDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateLocalPartnerInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// LocalPartners/UpdateMany
    /// </summary>
    /// <returns>List Of LocalPartnerDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateLocalPartnerInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// LocalPartners/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// LocalPartners/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }
}
