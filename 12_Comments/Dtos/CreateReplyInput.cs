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

    [Required(ErrorMessage = "Text is required")]
    [StringLength(2000, MinimumLength = 5, ErrorMessage = "Text Must be between 5 and 2000 characters")]
    public string Text { get; set; }

    [Required(ErrorMessage = "CommentId is required")]
    [RegularExpression(
        @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
        ErrorMessage = "Not a valid CommentId value"
    )]
    public Guid CommentId { get; set; }
}
