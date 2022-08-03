using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Videos;

public class VideosController : Controller
{
    private readonly IVideoService _service;
    public VideosController(IVideoService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of VideoDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of VideoDto</returns>
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
    /// Video/Find/[id]
    /// </summary>
    /// <returns>VideoDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// Video/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of VideoDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// Video/Add
    /// </summary>
    /// <returns>VideoDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateVideoInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// Video/AddMany
    /// </summary>
    /// <returns>List Of VideoDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateVideoInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Video/Update
    /// </summary>
    /// <returns>VideoDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateVideoInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// Video/UpdateMany
    /// </summary>
    /// <returns>List Of VideoDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateVideoInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Video/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// Video/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }
}
