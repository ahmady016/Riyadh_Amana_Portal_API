using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Users;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of UserDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of UserDto</returns>
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
    /// Users/Find/[id]
    /// </summary>
    /// <returns>UserDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// Users/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of UserDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// Users/Add
    /// </summary>
    /// <returns>UserDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateUserInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// Users/AddMany
    /// </summary>
    /// <returns>List Of UserDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateUserInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Users/Update
    /// </summary>
    /// <returns>UserDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateUserInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// Users/UpdateMany
    /// </summary>
    /// <returns>List Of UserDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateUserInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Users/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// Users/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }

}
