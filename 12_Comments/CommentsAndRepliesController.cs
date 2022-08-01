using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Comments;

[ApiController]
[Route("api/[controller]/[action]")]
public class CommentsAndRepliesController : ControllerBase
{
    private readonly ICommentAndReplyService _service;
    public CommentsAndRepliesController(ICommentAndReplyService service)
    {
        _service = service;
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// AlbumsAndPhotos/ListAlbums/all
    /// </summary>
    /// <returns>List of AlbumDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListAlbums([FromRoute] string type)
    {
        return Ok(_service.ListComments(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// AlbumsAndPhotos/ListAlbumsPage/all
    /// </summary>
    /// <returns>List of AlbumDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListCommentsPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListCommentsPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// AlbumsAndPhotos/FindAlbum/[id]
    /// </summary>
    /// <returns>AlbumDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindComment(Guid id)
    {
        return Ok(_service.FindOneComment(id));
    }
    /// <summary>
    /// AlbumsAndPhotos/FindAlbums/[id, id, id]
    /// </summary>
    /// <returns>List Of AlbumDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindAlbums(string ids)
    {
        return Ok(_service.FindManyComments(ids));
    }

    /// <summary>
    /// AlbumsAndPhotos/AddAlbum
    /// </summary>
    /// <returns>AlbumDto</returns>
    [HttpPost]
    public virtual IActionResult AddAlbum([FromBody] CreateCommentInput input)
    {
        return Ok(_service.AddComment(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/AddManyAlbums
    /// </summary>
    /// <returns>List Of AlbumDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyComments([FromBody] List<CreateCommentInput> inputs)
    {
        return Ok(_service.AddManyComments(inputs));
    }

    /// <summary>
    /// AlbumsAndPhotos/UpdateAlbum
    /// </summary>
    /// <returns>AlbumDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateComment([FromBody] UpdateCommentInput input)
    {
        return Ok(_service.UpdateComment(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/UpdateManyAlbums
    /// </summary>
    /// <returns>List Of AlbumDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyComments([FromBody] List<UpdateCommentInput> inputs)
    {
        return Ok(_service.UpdateManyComments(inputs));
    }

    /// <summary>
    /// AlbumsAndPhotos/DeleteAlbum
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteComment(Guid id)
    {
        return Ok(_service.DeleteComment(id));
    }
    /// <summary>
    /// AlbumsAndPhotos/ActivateAlbum
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateComment(Guid id)
    {
        return Ok(_service.ActivateComment(id));
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// AlbumsAndPhotos/ListPhotos/all
    /// </summary>
    /// <returns>List of PhotoDto</returns>
    /// 

    //----------------------------------------------------------------------------------------
    [HttpGet("{type}")]
    public virtual IActionResult ListPhotos([FromRoute] string type)
    {
        return Ok(_service.ListReplies(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// AlbumsAndPhotos/ListPhotosPage/all
    /// </summary>
    /// <returns>List of PhotoDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListPhotosPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListRepliesPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// AlbumsAndPhotos/FindPhoto/[id]
    /// </summary>
    /// <returns>PhotoDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindPhoto(Guid id)
    {
        return Ok(_service.FindOneReply(id));
    }
    /// <summary>
    /// AlbumsAndPhotos/FindPhotos/[id, id, id]
    /// </summary>
    /// <returns>List Of PhotoDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindReplies(string ids)
    {
        return Ok(_service.FindManyReplies(ids));
    }

    /// <summary>
    /// AlbumsAndPhotos/AddPhoto
    /// </summary>
    /// <returns>PhotoDto</returns>
    [HttpPost]
    public virtual IActionResult AddReply([FromBody] CreateReplyInput input)
    {
        return Ok(_service.AddReply(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/AddManyPhotos
    /// </summary>
    /// <returns>List Of PhotoDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyReplies([FromBody] List<CreateReplyInput> inputs)
    {
        return Ok(_service.AddManyReplies(inputs));
    }

    /// <summary>
    /// AlbumsAndPhotos/UpdatePhoto
    /// </summary>
    /// <returns>PhotoDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateReply([FromBody] UpdateReplyInput input)
    {
        return Ok(_service.UpdateReply(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/UpdateManyPhotos
    /// </summary>
    /// <returns>List Of PhotoDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyPhotos([FromBody] List<UpdateReplyInput> inputs)
    {
        return Ok(_service.UpdateManyReplies(inputs));
    }

    /// <summary>
    /// AlbumsAndPhotos/DeletePhoto
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteReply(Guid id)
    {
        return Ok(_service.DeleteReply(id));
    }
    /// <summary>
    /// AlbumsAndPhotos/ActivatePhoto
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateReply(Guid id)
    {
        return Ok(_service.ActivateReply(id));
    }
}
