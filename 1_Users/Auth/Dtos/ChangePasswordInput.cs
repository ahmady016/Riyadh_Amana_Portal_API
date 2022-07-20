using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos;

public class ChangePasswordInput
{
    [Required]
    [RegularExpression(
    @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
    ErrorMessage = "Not a valid Id value"
    )]
    public Guid UserId { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "OldPassword is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "OldPassword Must be between 8 and 50 characters")]
    public string OldPassword { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "NewPassword is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "NewPassword Must be between 8 and 50 characters")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "ConfirmNewPassword is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "ConfirmNewPassword Must be between 8 and 50 characters")]
    [Compare("NewPassword")]
    public string ConfirmNewPassword { get; set; }
}
