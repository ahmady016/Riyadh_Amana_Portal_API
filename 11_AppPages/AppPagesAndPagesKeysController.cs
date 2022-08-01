using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace AppPages;

[ApiController]
[Route("api/[controller]/[action]")]
public class AppPagesAndPagesKeysController : ControllerBase
{
    private readonly IAppPageAndPageKeyService _service;
    public AppPagesAndPagesKeysController(IAppPageAndPageKeyService service)
    {
        _service = service;
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// AppPagesAndPagesKeys/ListAppPages/all
    /// </summary>
    /// <returns>List of AppPageDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListAppPages([FromRoute] string type)
    {
        return Ok(_service.ListAppPages(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// AppPagesAndPagesKeys/ListAppPagesPage/all
    /// </summary>
    /// <returns>List of AppPageDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListAppPagesPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListAppPagesPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/FindAppPage/[id]
    /// </summary>
    /// <returns>AppPageDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindAppPage(Guid id)
    {
        return Ok(_service.FindOneAppPage(id));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/FindAppPages/[id, id, id]
    /// </summary>
    /// <returns>List Of AppPageDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindAppPages(string ids)
    {
        return Ok(_service.FindManyAppPages(ids));
    }

    /// <summary>
    /// AppPagesAndPagesKeys/AddAppPage
    /// </summary>
    /// <returns>AppPageDto</returns>
    [HttpPost]
    public virtual IActionResult AddAppPage([FromBody] CreateAppPageInput input)
    {
        return Ok(_service.AddAppPage(input));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/AddAppPageWithKeys
    /// </summary>
    /// <returns>AppPageDto</returns>
    [HttpPost]
    public virtual IActionResult AddAppPageWithKeys([FromBody] CreateAppPageWithKeysInput input)
    {
        return Ok(_service.AddAppPageWithKeys(input));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/AddManyAppPages
    /// </summary>
    /// <returns>List Of AppPageDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyAppPages([FromBody] List<CreateAppPageInput> inputs)
    {
        return Ok(_service.AddManyAppPages(inputs));
    }

    /// <summary>
    /// AppPagesAndPagesKeys/UpdateAppPage
    /// </summary>
    /// <returns>AppPageDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateAppPage([FromBody] UpdateAppPageInput input)
    {
        return Ok(_service.UpdateAppPage(input));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/UpdateManyAppPages
    /// </summary>
    /// <returns>List Of AppPageDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyAppPages([FromBody] List<UpdateAppPageInput> inputs)
    {
        return Ok(_service.UpdateManyAppPages(inputs));
    }

    /// <summary>
    /// AppPagesAndPagesKeys/DeleteAppPage
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteAppPage(Guid id)
    {
        return Ok(_service.DeleteAppPage(id));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/ActivateAppPage
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateAppPage(Guid id)
    {
        return Ok(_service.ActivateAppPage(id));
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// AppPagesAndPagesKeys/ListPageKeys/all
    /// </summary>
    /// <returns>List of PageKeyDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListPageKeys([FromRoute] string type)
    {
        return Ok(_service.ListPageKeys(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// AppPagesAndPagesKeys/ListPageKeysPage/all
    /// </summary>
    /// <returns>List of PageKeyDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListPageKeysPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListPageKeysPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/FindPageKey/[id]
    /// </summary>
    /// <returns>PageKeyDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindPageKey(Guid id)
    {
        return Ok(_service.FindOnePageKey(id));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/FindPageKeys/[id, id, id]
    /// </summary>
    /// <returns>List Of PageKeyDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindPageKeys(string ids)
    {
        return Ok(_service.FindManyPageKeys(ids));
    }

    /// <summary>
    /// AppPagesAndPagesKeys/AddPageKey
    /// </summary>
    /// <returns>PageKeyDto</returns>
    [HttpPost]
    public virtual IActionResult AddPageKey([FromBody] CreatePageKeyInput input)
    {
        return Ok(_service.AddPageKey(input));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/AddManyPageKeys
    /// </summary>
    /// <returns>List Of PageKeyDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyPageKeys([FromBody] List<CreatePageKeyInput> inputs)
    {
        return Ok(_service.AddManyPageKeys(inputs));
    }

    /// <summary>
    /// AppPagesAndPagesKeys/UpdatePageKey
    /// </summary>
    /// <returns>PageKeyDto</returns>
    [HttpPut]
    public virtual IActionResult UpdatePageKey([FromBody] UpdatePageKeyInput input)
    {
        return Ok(_service.UpdatePageKey(input));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/UpdateManyPageKeys
    /// </summary>
    /// <returns>List Of PageKeyDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyPageKeys([FromBody] List<UpdatePageKeyInput> inputs)
    {
        return Ok(_service.UpdateManyPageKeys(inputs));
    }

    /// <summary>
    /// AppPagesAndPagesKeys/DeletePageKey
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeletePageKey(Guid id)
    {
        return Ok(_service.DeletePageKey(id));
    }
    /// <summary>
    /// AppPagesAndPagesKeys/ActivatePageKey
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivatePageKey(Guid id)
    {
        return Ok(_service.ActivatePageKey(id));
    }

}
