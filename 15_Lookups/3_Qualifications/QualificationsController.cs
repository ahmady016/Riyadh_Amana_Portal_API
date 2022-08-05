using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Lookups;

[ApiController]
[Route("api/[controller]/[action]")]
public class QualificationsController : ControllerBase
{
    private readonly IQualificationService _service;
    public QualificationsController(IQualificationService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// Qualifications/List/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// Qualifications/ListPage/all
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
    /// Qualifications/FindOne/[id]
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindOne(Guid id)
    {
        return Ok(_service.FindOne(id));
    }
    /// <summary>
    /// Qualifications/FindMany/[id, id, id]
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindMany(string ids)
    {
        return Ok(_service.FindMany(ids));
    }

    /// <summary>
    /// Qualifications/Add
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateLookupInput input)
    {
        return Ok(_service.Add(input));
    }
    /// <summary>
    /// Qualifications/AddMany
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateLookupInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Qualifications/Update
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateLookupInput input)
    {
        return Ok(_service.Update(input));
    }
    /// <summary>
    /// Qualifications/UpdateMany
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateLookupInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Qualifications/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }
    /// <summary>
    /// Qualifications/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }

}
