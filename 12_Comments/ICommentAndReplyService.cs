using DB.Common;
using Dtos;

namespace Comments;

public interface ICommentAndReplyService
{
    List<CommentDto> ListComments(string type);
    PageResult<CommentDto> ListCommentsPage(string type, int pageSize, int pageNumber);
    CommentDto FindOneComment(Guid id);
    List<CommentDto> FindManyComments(string ids);
    CommentDto AddComment(CreateCommentInput input);
    List<CommentDto> AddManyComments(List<CreateCommentInput> inputs);
    CommentDto UpdateComment(UpdateCommentInput input);
    List<CommentDto> UpdateManyComments(List<UpdateCommentInput> inputs);
    bool DeleteComment(Guid id);
    bool ActivateComment(Guid id);
    //-------------------------------------------------------------------------------
    List<ReplyDto> ListReplies(string type);
    PageResult<ReplyDto> ListRepliesPage(string type, int pageSize, int pageNumber);
    ReplyDto FindOneReply(Guid id);
    List<ReplyDto> FindManyReplies(string ids);
    ReplyDto AddReply(CreateReplyInput input);
    List<ReplyDto> AddManyReplies(List<CreateReplyInput> inputs);
    ReplyDto UpdateReply(UpdateReplyInput input);
    List<ReplyDto> UpdateManyReplies(List<UpdateReplyInput> inputs);
    bool DeleteReply(Guid id);
    bool ActivateReply(Guid id);
}
