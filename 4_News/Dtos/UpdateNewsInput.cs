using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class UpdateNewsInput
{
    [Required]
    [RegularExpression(
        @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
        ErrorMessage = "Not a valid Id value"
    )]
    public Guid Id { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    [Required]
    public string CreatedBy { get; set; }
}
