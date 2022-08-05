using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Lookups;

[ApiController]
[Route("api/[controller]/[action]")]
public class NationalitiesController : ControllerBase
{
    private readonly INationalityService _service;
    public NationalitiesController(INationalityService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// Nationalities/List/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// Nationalities/ListPage/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
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
    /// Nationalities/FindOne/[id]
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindOne(Guid id)
    {
        return Ok(_service.FindOne(id));
    }
    /// <summary>
    /// Nationalities/FindMany/[id, id, id]
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindMany(string ids)
    {
        return Ok(_service.FindMany(ids));
    }

    /// <summary>
    /// Nationalities/Add
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateLookupInput input)
    {
        return Ok(_service.Add(input));
    }
    /// <summary>
    /// Nationalities/AddMany
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateLookupInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Nationalities/Update
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateLookupInput input)
    {
        return Ok(_service.Update(input));
    }
    /// <summary>
    /// Nationalities/UpdateMany
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateLookupInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Nationalities/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }
    /// <summary>
    /// Nationalities/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }

}
