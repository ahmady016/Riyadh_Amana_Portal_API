using DB.Entities;
using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateCommentInput
{
    [Required(ErrorMessage = "EntityName is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "EntityName Must be between 5 and 100 characters")]
    public string EntityName { get; set; }
    [Required(ErrorMessage = "CommenterName is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "CommenterName Must be between 5 and 100 characters")]
    public string CommenterName { get; set; }

    [Required(ErrorMessage = "CommenterEmail is required")]
    [StringLength(100, MinimumLength = 15, ErrorMessage = "CommenterEmail Must be between 15 and 100 characters")]
    public string CommenterEmail { get; set; }

    [Required(ErrorMessage = "Text is required")]
    [StringLength(2000, MinimumLength = 5, ErrorMessage = "Text Must be between 5 and 2000 characters")]
    public string Text { get; set; }
}
