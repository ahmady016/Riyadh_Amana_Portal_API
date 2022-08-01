using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dtos;
using Auth.Dtos;

namespace Users;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    private string GetIPAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Register(RegisterInput input)
    {
        var result = _service.Register(input, GetIPAddress());
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login(LoginDto login)
    {
        var result = _service.Login(login, GetIPAddress());
        return Ok(result);
    }

    [HttpGet("{token}")]
    public IActionResult RefreshToken([FromRoute] string token)
    {
        var result = _service.RefreshTheTokens(token, GetIPAddress());
        return Ok(result);
    }
    [HttpGet("{token}")]
    public IActionResult RevokeToken([FromRoute] string token)
    {
        _service.RevokeTheToken(token, GetIPAddress());
        return Ok(new { Message = "Token is Revoked" });
    }

    [HttpPost]
    public IActionResult ChangePassword(ChangePasswordInput input)
    {
        _service.ChangePassword(input);
        return Ok(new { Message = "Password Changed Successfully" });
    }

    [HttpPost]
    public IActionResult ChangeEmail(ChangeEmailInput input)
    {
        _service.ChangeEmail(input);
        return Ok(new { Message = "Email Changed Successfully" });
    }

    [HttpGet("{userId}")]
    public IActionResult Logout(Guid userId)
    {
        bool isLoggedout = _service.Logout(userId);
        var result = new { Message = isLoggedout ? "User is logedout ..." : "User aleary logedout!!!" };
        return Ok(result);
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
    [HttpDelete("{id}")]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// Users/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut("{id}")]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
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
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

}
