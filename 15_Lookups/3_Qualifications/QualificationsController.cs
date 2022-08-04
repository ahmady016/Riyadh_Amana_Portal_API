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
    /// Qualifications/ListQualifications/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListQualifications([FromRoute] string type)
    {
        return Ok(_service.ListQualifications(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// Qualifications/ListQualificationsPage/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListQualificationsPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListQualificationsPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// Qualifications/FindQualification/[id]
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindQualification(Guid id)
    {
        return Ok(_service.FindOneQualification(id));
    }
    /// <summary>
    /// Qualifications/FindQualifications/[id, id, id]
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindQualifications(string ids)
    {
        return Ok(_service.FindManyQualifications(ids));
    }

    /// <summary>
    /// Qualifications/AddQualification
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddQualification([FromBody] CreateLookupInput input)
    {
        return Ok(_service.AddQualification(input));
    }
    /// <summary>
    /// Qualifications/AddManyQualifications
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyQualifications([FromBody] List<CreateLookupInput> inputs)
    {
        return Ok(_service.AddManyQualifications(inputs));
    }

    /// <summary>
    /// Qualifications/UpdateQualification
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateQualification([FromBody] UpdateLookupInput input)
    {
        return Ok(_service.UpdateQualification(input));
    }
    /// <summary>
    /// Qualifications/UpdateManyQualifications
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyQualifications([FromBody] List<UpdateLookupInput> inputs)
    {
        return Ok(_service.UpdateManyQualifications(inputs));
    }

    /// <summary>
    /// Qualifications/DeleteQualification
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteQualification(Guid id)
    {
        return Ok(_service.DeleteQualification(id));
    }
    /// <summary>
    /// Qualifications/ActivateQualification
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateQualification(Guid id)
    {
        return Ok(_service.ActivateQualification(id));
    }
}
