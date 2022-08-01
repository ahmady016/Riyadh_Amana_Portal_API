using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;
using System.Net;

namespace Comments;

public class CommentAndReplyService : ICommentAndReplyService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Comment> _logger;
    private string _errorMessage;

    public CommentAndReplyService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Comment> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    #region private Methods
    private Comment GetCommentById(Guid id)
    {
        var album = _crudService.Find<Comment, Guid>(id);
        if (album is null)
        {
            _errorMessage = $"Comment Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return album;
    }
    private List<Comment> GetCommentsByIds(List<Guid> ids)
    {
        var comments = _crudService.GetList<Comment, Guid>(e => ids.Contains(e.Id));
        if (comments.Count == 0)
        {
            _errorMessage = $"No Any Comment Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return comments;
    }
    private static void FillRestPropsWithOldValues(Comment oldItem, Comment newItem)
    {
        newItem.CreatedAt = oldItem.CreatedAt;
        newItem.CreatedBy = oldItem.CreatedBy;
        newItem.UpdatedAt = oldItem.UpdatedAt;
        newItem.UpdatedBy = oldItem.UpdatedBy;
        newItem.IsActive = oldItem.IsActive;
        newItem.ActivatedAt = oldItem.ActivatedAt;
        newItem.ActivatedBy = oldItem.ActivatedBy;
        newItem.IsDeleted = oldItem.IsDeleted;
        newItem.DeletedAt = oldItem.DeletedAt;
        newItem.DeletedBy = oldItem.DeletedBy;
    }

    private Reply GetReplyById(Guid id)
    {
        var reply = _crudService.Find<Reply, Guid>(id);
        if (reply is null)
        {
            _errorMessage = $"Reply Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return reply;
    }
    private List<Reply> GetRepliesByIds(List<Guid> ids)
    {
        var replies = _crudService.GetList<Reply, Guid>(e => ids.Contains(e.Id));
        if (replies.Count == 0)
        {
            _errorMessage = $"No Any Reply Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return replies;
    }
    private static void FillRestPropsWithOldValues(Reply oldItem, Reply newItem)
    {
        newItem.CreatedAt = oldItem.CreatedAt;
        newItem.CreatedBy = oldItem.CreatedBy;
        newItem.UpdatedAt = oldItem.UpdatedAt;
        newItem.UpdatedBy = oldItem.UpdatedBy;
        newItem.IsActive = oldItem.IsActive;
        newItem.ActivatedAt = oldItem.ActivatedAt;
        newItem.ActivatedBy = oldItem.ActivatedBy;
        newItem.IsDeleted = oldItem.IsDeleted;
        newItem.DeletedAt = oldItem.DeletedAt;
        newItem.DeletedBy = oldItem.DeletedBy;
    }
    #endregion

    public List<CommentDto> ListComments(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Comment, Guid>(),
            "deleted" => _crudService.GetList<Comment, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Comment, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<CommentDto>>(list);
    }
    public PageResult<CommentDto> ListCommentsPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Comment>(),
            "deleted" => _crudService.GetQuery<Comment>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Comment>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<CommentDto>()
        {
            PageItems = _mapper.Map<List<CommentDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public CommentDto FindOneComment(Guid id)
    {
        var comment = GetCommentById(id);
        return _mapper.Map<CommentDto>(comment);
    }
    public List<CommentDto> FindManyComments(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"Comment: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetCommentsByIds(_ids);
        return _mapper.Map<List<CommentDto>>(list);
    }

    public CommentDto AddComment(CreateCommentInput input)
    {
        var comment = _mapper.Map<Comment>(input);
        var createdcomment = _crudService.Add<Comment, Guid>(comment);
        _crudService.SaveChanges();
        return _mapper.Map<CommentDto>(createdcomment);
    }
    public List<CommentDto> AddManyComments(List<CreateCommentInput> inputs)
    {
        var comments = _mapper.Map<List<Comment>>(inputs);
        var createdcomments = _crudService.AddAndGetRange<Comment, Guid>(comments);
        _crudService.SaveChanges();
        return _mapper.Map<List<CommentDto>>(createdcomments);
    }

    public CommentDto UpdateComment(UpdateCommentInput input)
    {
        var oldcomment = GetCommentById(input.Id);
        var newcomment = _mapper.Map<Comment>(input);

        FillRestPropsWithOldValues(oldcomment, newcomment);
        var updatedAlbum = _crudService.Update<Comment, Guid>(newcomment);
        _crudService.SaveChanges();

        return _mapper.Map<CommentDto>(updatedAlbum);
    }
    public List<CommentDto> UpdateManyComments(List<UpdateCommentInput> inputs)
    {
        var oldAlbums = GetCommentsByIds(inputs.Select(x => x.Id).ToList());
        var newAlbums = _mapper.Map<List<Comment>>(inputs);

        for (int i = 0; i < oldAlbums.Count; i++)
            FillRestPropsWithOldValues(oldAlbums[i], newAlbums[i]);
        var updatedAlbums = _crudService.UpdateAndGetRange<Comment, Guid>(newAlbums);
        _crudService.SaveChanges();

        return _mapper.Map<List<CommentDto>>(updatedAlbums);
    }

    public bool DeleteComment(Guid id)
    {
        var album = GetCommentById(id);
        _crudService.SoftDelete<Comment, Guid>(album);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateComment(Guid id)
    {
        var album = GetCommentById(id);
        _crudService.Activate<Comment, Guid>(album);
        _crudService.SaveChanges();
        return true;
    }
    
    public List<ReplyDto> ListReplies(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Reply, Guid>(),
            "deleted" => _crudService.GetList<Reply, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Reply, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<ReplyDto>>(list);
    }
    public PageResult<ReplyDto> ListRepliesPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Reply>(),
            "deleted" => _crudService.GetQuery<Reply>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Reply>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<ReplyDto>()
        {
            PageItems = _mapper.Map<List<ReplyDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public ReplyDto FindOneReply(Guid id)
    {
        var photo = GetReplyById(id);
        return _mapper.Map<ReplyDto>(photo);
    }
    public List<ReplyDto> FindManyReplies(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"Reply: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetRepliesByIds(_ids);
        return _mapper.Map<List<ReplyDto>>(list);
    }

    public ReplyDto AddReply(CreateReplyInput input)
    {
        var photo = _mapper.Map<Reply>(input);
        var createdPhoto = _crudService.Add<Reply, Guid>(photo);
        _crudService.SaveChanges();
        return _mapper.Map<ReplyDto>(createdPhoto);
    }
    public List<ReplyDto> AddManyReplies(List<CreateReplyInput> inputs)
    {
        var photos = _mapper.Map<List<Reply>>(inputs);
        var createdPhotos = _crudService.AddAndGetRange<Reply, Guid>(photos);
        _crudService.SaveChanges();
        return _mapper.Map<List<ReplyDto>>(createdPhotos);
    }

    public ReplyDto UpdateReply(UpdateReplyInput input)
    {
        var oldPhoto = GetReplyById(input.Id);
        var newPhoto = _mapper.Map<Reply>(input);

        FillRestPropsWithOldValues(oldPhoto, newPhoto);
        var updatedPhoto = _crudService.Update<Reply, Guid>(newPhoto);
        _crudService.SaveChanges();

        return _mapper.Map<ReplyDto>(updatedPhoto);
    }
    public List<ReplyDto> UpdateManyReplies(List<UpdateReplyInput> inputs)
    {
        var oldPhotos = GetRepliesByIds(inputs.Select(x => x.Id).ToList());
        var newPhotos = _mapper.Map<List<Reply>>(inputs);

        for (int i = 0; i < oldPhotos.Count; i++)
            FillRestPropsWithOldValues(oldPhotos[i], newPhotos[i]);
        var updatedPhotos = _crudService.UpdateAndGetRange<Reply, Guid>(newPhotos);
        _crudService.SaveChanges();

        return _mapper.Map<List<ReplyDto>>(updatedPhotos);
    }

    public bool DeleteReply(Guid id)
    {
        var photo = GetReplyById(id);
        _crudService.SoftDelete<Reply, Guid>(photo);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateReply(Guid id)
    {
        var reply = GetReplyById(id);
        _crudService.Activate<Reply, Guid>(reply);
        _crudService.SaveChanges();
        return true;
    }

}
