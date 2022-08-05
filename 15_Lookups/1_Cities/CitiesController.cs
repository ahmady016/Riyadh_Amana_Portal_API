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
    /// Cities/ListCities/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListCities([FromRoute] string type)
    {
        return Ok(_service.ListCities(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// Cities/ListCitiesPage/all
    /// </summary>
    /// <returns>List of LookupDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListCitiesPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListCitiesPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// Cities/FindCity/[id]
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindCity(Guid id)
    {
        return Ok(_service.FindOneCity(id));
    }
    /// <summary>
    /// Cities/FindCities/[id, id, id]
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindCities(string ids)
    {
        return Ok(_service.FindManyCities(ids));
    }

    /// <summary>
    /// Cities/AddCity
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddCity([FromBody] CreateLookupInput input)
    {
        return Ok(_service.AddCity(input));
    }
    /// <summary>
    /// Cities/AddManyCities
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyCities([FromBody] List<CreateLookupInput> inputs)
    {
        return Ok(_service.AddManyCities(inputs));
    }

    /// <summary>
    /// Cities/UpdateCity
    /// </summary>
    /// <returns>LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateCity([FromBody] UpdateLookupInput input)
    {
        return Ok(_service.UpdateCity(input));
    }
    /// <summary>
    /// Cities/UpdateManyCities
    /// </summary>
    /// <returns>List Of LookupDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyCities([FromBody] List<UpdateLookupInput> inputs)
    {
        return Ok(_service.UpdateManyCities(inputs));
    }

    /// <summary>
    /// Cities/DeleteCity
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteCity(Guid id)
    {
        return Ok(_service.DeleteCity(id));
    }
    /// <summary>
    /// Cities/ActivateCity
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateCity(Guid id)
    {
        return Ok(_service.ActivateCity(id));
    }
}
