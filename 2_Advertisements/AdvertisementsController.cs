using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Advertisements;

[ApiController]
[Route("api/[controller]/[action]")]
public class AdvertisementsController : ControllerBase
{
    private readonly IAdvertisementService _service;
    public AdvertisementsController(IAdvertisementService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of AdvertisementDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of AdvertisementDto</returns>
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
    /// Advertisements/Find/[id]
    /// </summary>
    /// <returns>AdvertisementDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// Advertisements/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of AdvertisementDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// Advertisements/Add
    /// </summary>
    /// <returns>AdvertisementDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateAdvertisementInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// Advertisements/AddMany
    /// </summary>
    /// <returns>List Of AdvertisementDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateAdvertisementInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Advertisements/Update
    /// </summary>
    /// <returns>AdvertisementDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateAdvertisementInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// Advertisements/UpdateMany
    /// </summary>
    /// <returns>List Of AdvertisementDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateAdvertisementInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Advertisements/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// Advertisements/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }

}
