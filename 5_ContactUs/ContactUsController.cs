using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace _ContactUs;

[ApiController]
[Route("api/[controller]/[action]")]
public class ContactUsController : Controller
{
    private readonly IContactUsService _service;
    public ContactUsController(IContactUsService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// ContactUs/List/all
    /// </summary>
    /// <returns>List of ContactUsDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// ContactUs/ListPage/existed
    /// </summary>
    /// <returns>List of ContactUsDto</returns>
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
    /// ContactUs/Find/[id]
    /// </summary>
    /// <returns>ContactUsDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// ContactUs/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of ContactUsDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// ContactUs/Add
    /// </summary>
    /// <returns>ContactUsDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateContactUsInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// ContactUs/AddMany
    /// </summary>
    /// <returns>List Of ContactUsDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateContactUsInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// ContactUs/Update
    /// </summary>
    /// <returns>ContactUsDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateContactUsInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// ContactUs/UpdateMany
    /// </summary>
    /// <returns>List Of ContactUsDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateContactUsInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// ContactUs/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// ContactUs/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }
}
