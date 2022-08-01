using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateReplyInput
{
    [Required(ErrorMessage = "ReplierName is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "ReplierName Must be between 5 and 100 characters")]
    public string ReplierName { get; set; }
    [Required(ErrorMessage = "ReplierEmail is required")]
    [StringLength(100, MinimumLength = 15, ErrorMessage = "ReplierEmail Must be between 15 and 100 characters")]
    public string ReplierEmail { get; set; }

    [Required]
    [StringLength(2000)]
    public string Text { get; set; }
    public bool IsApproved { get; set; } = false;
    public DateTime? ApprovedAt { get; set; }
    public string ApprovedBy { get; set; }
    public Guid CommentId { get; set; }
}
