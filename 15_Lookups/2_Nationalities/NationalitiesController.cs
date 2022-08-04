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
    /// Nationalities/ListNationalities/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListNationalities([FromRoute] string type)
    {
        return Ok(_service.ListNationalities(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// Nationalities/ListNationalitiesPage/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListNationalitiesPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListNationalitiesPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// Nationalities/FindNationality/[id]
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindNationality(Guid id)
    {
        return Ok(_service.FindOneNationality(id));
    }
    /// <summary>
    /// Nationalities/FindNationalities/[id, id, id]
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindNationalities(string ids)
    {
        return Ok(_service.FindManyNationalities(ids));
    }

    /// <summary>
    /// Nationalities/AddNationality
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddNationality([FromBody] CreateLookupInput input)
    {
        return Ok(_service.AddNationality(input));
    }
    /// <summary>
    /// Nationalities/AddManyNationalities
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyNationalities([FromBody] List<CreateLookupInput> inputs)
    {
        return Ok(_service.AddManyNationalities(inputs));
    }

    /// <summary>
    /// Nationalities/UpdateNationality
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateNationality([FromBody] UpdateLookupInput input)
    {
        return Ok(_service.UpdateNationality(input));
    }
    /// <summary>
    /// Nationalities/UpdateManyNationalities
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyNationalities([FromBody] List<UpdateLookupInput> inputs)
    {
        return Ok(_service.UpdateManyNationalities(inputs));
    }

    /// <summary>
    /// Nationalities/DeleteNationality
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteNationality(Guid id)
    {
        return Ok(_service.DeleteNationality(id));
    }
    /// <summary>
    /// Nationalities/ActivateNationality
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateNationality(Guid id)
    {
        return Ok(_service.ActivateNationality(id));
    }
}
