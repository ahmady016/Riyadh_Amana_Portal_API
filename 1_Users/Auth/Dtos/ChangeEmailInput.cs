using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos;

public class ChangeEmailInput
{
    [Required]
    [RegularExpression(
        @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
        ErrorMessage = "Not a valid Id value"
    )]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "OldEmail is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "OldEmail Must be between 6 and 100 characters")]
    [EmailAddress(ErrorMessage = "OldEmail Must be a valid email")]
    public string OldEmail { get; set; }

    [Required(ErrorMessage = "NewEmail is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "NewEmail Must be between 6 and 100 characters")]
    [EmailAddress(ErrorMessage = "NewEmail Must be a valid email")]
    public string NewEmail { get; set; }
}
