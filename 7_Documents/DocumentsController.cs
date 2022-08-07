using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Documents;

[ApiController]
[Route("api/[controller]/[action]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _service;
    public DocumentsController(IDocumentService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of DocumentDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of DocumentDto</returns>
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
    /// Document/Find/[id]
    /// </summary>
    /// <returns>DocumentDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// Document/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of DocumentDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// Document/Add
    /// </summary>
    /// <returns>DocumentDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateDocumentInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// Document/AddMany
    /// </summary>
    /// <returns>List Of DocumentDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateDocumentInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Document/Update
    /// </summary>
    /// <returns>DocumentDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateDocumentInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// Document/UpdateMany
    /// </summary>
    /// <returns>List Of DocumentDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateDocumentInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Document/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// Document/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }

}
