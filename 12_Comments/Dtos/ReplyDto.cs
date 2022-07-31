namespace Dtos;

public class ReplyDto
{
    public Guid Id { get; set; }
    public string ReplierName { get; set; }
    public string ReplierEmail { get; set; }
    public string Text { get; set; }
    public bool IsApproved { get; set; } = false;
    public DateTime? ApprovedAt { get; set; }
    public string ApprovedBy { get; set; }
    public Guid CommentId { get; set; }
}
