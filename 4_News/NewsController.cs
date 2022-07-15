using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace _News;

[ApiController]
[Route("api/[controller]/[action]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _service;
    public NewsController(INewsService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// News/List/all
    /// </summary>
    /// <returns>List of NewsDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// News/ListPage/existed
    /// </summary>
    /// <returns>List of NewsDto</returns>
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
    /// News/Find/[id]
    /// </summary>
    /// <returns>NewsDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// News/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of NewsDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// News/Add
    /// </summary>
    /// <returns>NewsDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateNewsInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// News/AddMany
    /// </summary>
    /// <returns>List Of NewsDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateNewsInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// News/Update
    /// </summary>
    /// <returns>NewsDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateNewsInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// News/UpdateMany
    /// </summary>
    /// <returns>List Of NewsDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateNewsInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// News/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// News/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }

}
