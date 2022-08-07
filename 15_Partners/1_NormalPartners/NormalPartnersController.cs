using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Partners;

[ApiController]
[Route("api/[controller]/[action]")]
public class NormalPartnersController : ControllerBase
{
    private readonly INormalPartnerService _service;
    public NormalPartnersController(INormalPartnerService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of NormalPartnerDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of NormalPartnerDto</returns>
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
    /// NormalPartners/Find/[id]
    /// </summary>
    /// <returns>NormalPartnerDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// NormalPartners/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of NormalPartnerDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// NormalPartners/Add
    /// </summary>
    /// <returns>NormalPartnerDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateNormalPartnerInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// NormalPartners/AddMany
    /// </summary>
    /// <returns>List Of NormalPartnerDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateNormalPartnerInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// NormalPartners/Update
    /// </summary>
    /// <returns>NormalPartnerDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateNormalPartnerInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// NormalPartners/UpdateMany
    /// </summary>
    /// <returns>List Of NormalPartnerDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateNormalPartnerInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// NormalPartners/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// NormalPartners/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }

}
