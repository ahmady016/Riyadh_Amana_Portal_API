using DB.Entities;

namespace Dtos;

public class CommentDto
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; }
    public string EntityName { get; set; }
    public string CommenterName { get; set; }
    public string CommenterEmail { get; set; }
    public string Text { get; set; }
    public bool IsApproved { get; set; } = false;
    public DateTime? ApprovedAt { get; set; }
    public string ApprovedBy { get; set; }

    public List<Reply> Replies { get; set; }
}
