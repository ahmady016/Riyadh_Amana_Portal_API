
using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateContactUsInput
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Name Must be between 6 and 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Email Must be between 6 and 100 characters")]
    [EmailAddress(ErrorMessage = "Email Must be a valid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Mobile is required")]
    [StringLength(20, MinimumLength = 10, ErrorMessage = "Mobile Must be between 10 and 20 characters")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
    public string Mobile { get; set; }

    [StringLength(200, MinimumLength = 10, ErrorMessage = "Address Must be between 10 and 200 characters")]
    public string Address { get; set; }

    [StringLength(20, MinimumLength = 5, ErrorMessage = "Longitude Must be between 5 and 20 characters")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Longitude must be numeric")]
    public string Longitude { get; set; }

    [StringLength(20, MinimumLength = 5, ErrorMessage = "Latitude Must be between 5 and 20 characters")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Latitude must be numeric")]
    public string Latitude { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Title Must be between 5 and 100 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(400, MinimumLength = 10, ErrorMessage = "Description Must be between 10 and 400 characters")]
    public string Description { get; set; }

    [Required(ErrorMessage = "FileUrl is required")]
    [StringLength(400, MinimumLength = 10, ErrorMessage = "FileUrl Must be between 10 and 400 characters")]
    public string FileUrl { get; set; }
}
