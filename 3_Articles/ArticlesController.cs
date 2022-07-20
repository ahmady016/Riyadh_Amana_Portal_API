using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Articles;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _service;
    public ArticlesController(IArticleService service)
    {
        _service = service;
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of ArticleDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult List([FromRoute] string type)
    {
        return Ok(_service.List(type));
    }

    /// <summary>
    /// listType values (all/deleted/existed)
    /// </summary>
    /// <returns>List of ArticleDto</returns>
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
    /// Articles/Find/[id]
    /// </summary>
    /// <returns>ArticleDto</returns>
    [HttpGet("{id}")]
    public IActionResult Find(Guid id)
    {
        return Ok(_service.Find(id));
    }

    /// <summary>
    /// Articles/FindList/[id, id, id]
    /// </summary>
    /// <returns>List Of ArticleDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindList(string ids)
    {
        return Ok(_service.FindList(ids));
    }

    /// <summary>
    /// Articles/Add
    /// </summary>
    /// <returns>ArticleDto</returns>
    [HttpPost]
    public virtual IActionResult Add([FromBody] CreateArticleInput input)
    {
        return Ok(_service.Add(input));
    }

    /// <summary>
    /// Articles/AddMany
    /// </summary>
    /// <returns>List Of ArticleDto</returns>
    [HttpPost]
    public virtual IActionResult AddMany([FromBody] List<CreateArticleInput> inputs)
    {
        return Ok(_service.AddMany(inputs));
    }

    /// <summary>
    /// Articles/Update
    /// </summary>
    /// <returns>ArticleDto</returns>
    [HttpPut]
    public virtual IActionResult Update([FromBody] UpdateArticleInput input)
    {
        return Ok(_service.Update(input));
    }

    /// <summary>
    /// Articles/UpdateMany
    /// </summary>
    /// <returns>List Of ArticleDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateMany([FromBody] List<UpdateArticleInput> inputs)
    {
        return Ok(_service.UpdateMany(inputs));
    }

    /// <summary>
    /// Articles/Delete
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult Delete(Guid id)
    {
        return Ok(_service.Delete(id));
    }

    /// <summary>
    /// Articles/Activate
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult Activate(Guid id)
    {
        return Ok(_service.Activate(id));
    }
}
