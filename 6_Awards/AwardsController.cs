using amana_mono._6_Awards.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Awards;

[ApiController]
[Route("api/[controller]/[action]")]
public class AwardsController : ControllerBase
{
    private readonly IAwardsService _service;
    public AwardsController(IAwardsService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of AwardsDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of AwardsDto</returns>
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
    /// Award/Find/[id]
    /// </summary>
    /// <returns>AwardsDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// Award/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of AwardsDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// Award/Add
    /// </summary>
    /// <returns>AwardsDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateAwardsInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// Award/AddMany
    /// </summary>
    /// <returns>List Of AwardsDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateAwardsInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Award/Update
    /// </summary>
    /// <returns>AwardsDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateAwardsInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// Award/UpdateMany
    /// </summary>
    /// <returns>List Of AwardsDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateAwardsInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Award/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// Award/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }

}
