using System.ComponentModel.DataAnnotations;
using DB.Entities;

namespace Auth.Dtos;

public class RegisterInput
{
    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(30, MinimumLength = 3)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    [StringLength(70, MinimumLength = 5)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    [Required(ErrorMessage = "NationalId is required")]
    [StringLength(15, MinimumLength = 10)]
    public string NationalId { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Email Must be between 6 and 100 characters")]
    [EmailAddress(ErrorMessage = "Email Must be a valid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password Must be between 8 and 50 characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "ConfirmPassword is required")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "ConfirmPassword Must be between 8 and 50 characters")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Mobile is required")]
    [StringLength(20, MinimumLength = 10, ErrorMessage = "Mobile Must be between 10 and 20 characters")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
    public string Mobile { get; set; }
}
