using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace AppFeatures;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticlesController : ControllerBase
{
    private readonly IAppFeatureService _service;
    public ArticlesController(IAppFeatureService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of AppFeatureDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of AppFeatureDto</returns>
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
    /// AppFeatures/Find/[id]
    /// </summary>
    /// <returns>AppFeatureDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// AppFeatures/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of AppFeatureDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// AppFeatures/Add
    /// </summary>
    /// <returns>AppFeatureDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateAppFeatureInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// AppFeatures/AddMany
    /// </summary>
    /// <returns>List Of AppFeatureDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateAppFeatureInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// AppFeatures/Update
    /// </summary>
    /// <returns>AppFeatureDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateAppFeatureInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// AppFeatures/UpdateMany
    /// </summary>
    /// <returns>List Of AppFeatureDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateAppFeatureInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// AppFeatures/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// AppFeatures/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }
}
