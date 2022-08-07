using System.ComponentModel.DataAnnotations;
using DB.Entities;

namespace Dtos;

public class UpdateUserInput
{
    [Required(ErrorMessage = "Id is required")]
    [RegularExpression(
        @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
        ErrorMessage = "Not a valid Id value"
    )]
    public Guid Id { get; set; }

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

    [Required(ErrorMessage = "Mobile is required")]
    [StringLength(20, MinimumLength = 10, ErrorMessage = "Mobile Must be between 10 and 20 characters")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
    public string Mobile { get; set; }
}
