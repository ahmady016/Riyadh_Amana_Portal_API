using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Lookups;

[ApiController]
[Route("api/[controller]/[action]")]
public class CitiesController : ControllerBase
{
    private readonly ICityService _service;
    public CitiesController(ICityService service)
    {
        _service = service;
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// Cities/List/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// Cities/ListPage/all
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
    /// Cities/FindOne/[id]
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindOne(Guid id)
    {
        return Ok(_service.FindOne(id));
    }
    /// <summary>
    /// Cities/FindMany/[id, id, id]
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindMany(string ids)
    {
        return Ok(_service.FindMany(ids));
    }

    /// <summary>
    /// Cities/Add
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateLookupInput input)
    {
        return Ok(_service.Add(input));
    }
    /// <summary>
    /// Cities/AddMany
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateLookupInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Cities/Update
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateLookupInput input)
    {
        return Ok(_service.Update(input));
    }
    /// <summary>
    /// Cities/UpdateMany
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateLookupInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Cities/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }
    /// <summary>
    /// Cities/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }
}
