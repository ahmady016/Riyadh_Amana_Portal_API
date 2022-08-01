using Microsoft.AspNetCore.Mvc;
using Dtos;

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
    /// CommentsAndReplies/ListComments/all
    /// </summary>
    /// <returns>List of CommentDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListComments([FromRoute] string type)
    {
        return Ok(_service.ListComments(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// CommentsAndReplies/ListCommentsPage/all
    /// </summary>
    /// <returns>List of CommentDto</returns>
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
    /// CommentsAndReplies/FindComment/[id]
    /// </summary>
    /// <returns>CommentDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindComment(Guid id)
    {
        return Ok(_service.FindOneComment(id));
    }
    /// <summary>
    /// CommentsAndReplies/FindComments/[id, id, id]
    /// </summary>
    /// <returns>List Of CommentDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindComments(string ids)
    {
        return Ok(_service.FindManyComments(ids));
    }

    /// <summary>
    /// CommentsAndReplies/AddComment
    /// </summary>
    /// <returns>CommentDto</returns>
    [HttpPost]
    public virtual IActionResult AddComment([FromBody] CreateCommentInput input)
    {
        return Ok(_service.AddComment(input));
    }
    /// <summary>
    /// CommentsAndReplies/AddManyComments
    /// </summary>
    /// <returns>List Of CommentDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyComments([FromBody] List<CreateCommentInput> inputs)
    {
        return Ok(_service.AddManyComments(inputs));
    }

    /// <summary>
    /// CommentsAndReplies/UpdateComment
    /// </summary>
    /// <returns>CommentDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateComment([FromBody] UpdateCommentInput input)
    {
        return Ok(_service.UpdateComment(input));
    }
    /// <summary>
    /// CommentsAndReplies/UpdateManyComments
    /// </summary>
    /// <returns>List Of CommentDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyComments([FromBody] List<UpdateCommentInput> inputs)
    {
        return Ok(_service.UpdateManyComments(inputs));
    }

    /// <summary>
    /// CommentsAndReplies/DeleteComment
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteComment(Guid id)
    {
        return Ok(_service.DeleteComment(id));
    }
    /// <summary>
    /// CommentsAndReplies/ActivateComment
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateComment(Guid id)
    {
        return Ok(_service.ActivateComment(id));
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// CommentsAndReplies/ListReplies/all
    /// </summary>
    /// <returns>List of ReplyDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListReplies([FromRoute] string type)
    {
        return Ok(_service.ListReplies(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// CommentsAndReplies/ListRepliesPage/all
    /// </summary>
    /// <returns>List of ReplyDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListRepliesPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListRepliesPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// CommentsAndReplies/FindReply/[id]
    /// </summary>
    /// <returns>ReplyDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindReply(Guid id)
    {
        return Ok(_service.FindOneReply(id));
    }
    /// <summary>
    /// CommentsAndReplies/FindReplies/[id, id, id]
    /// </summary>
    /// <returns>List Of ReplyDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindReplies(string ids)
    {
        return Ok(_service.FindManyReplies(ids));
    }

    /// <summary>
    /// CommentsAndReplies/AddReply
    /// </summary>
    /// <returns>ReplyDto</returns>
    [HttpPost]
    public virtual IActionResult AddReply([FromBody] CreateReplyInput input)
    {
        return Ok(_service.AddReply(input));
    }
    /// <summary>
    /// CommentsAndReplies/AddManyReplies
    /// </summary>
    /// <returns>List Of ReplyDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyReplies([FromBody] List<CreateReplyInput> inputs)
    {
        return Ok(_service.AddManyReplies(inputs));
    }

    /// <summary>
    /// CommentsAndReplies/UpdateReply
    /// </summary>
    /// <returns>ReplyDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateReply([FromBody] UpdateReplyInput input)
    {
        return Ok(_service.UpdateReply(input));
    }
    /// <summary>
    /// CommentsAndReplies/UpdateManyPhotos
    /// </summary>
    /// <returns>List Of ReplyDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyPhotos([FromBody] List<UpdateReplyInput> inputs)
    {
        return Ok(_service.UpdateManyReplies(inputs));
    }

    /// <summary>
    /// CommentsAndReplies/DeleteReply
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteReply(Guid id)
    {
        return Ok(_service.DeleteReply(id));
    }
    /// <summary>
    /// CommentsAndReplies/ActivateReply
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateReply(Guid id)
    {
        return Ok(_service.ActivateReply(id));
    }

}
