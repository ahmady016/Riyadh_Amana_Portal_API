using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class UpdatePartnersInput : CreatePartnersInput
{
    [Required]
    [RegularExpression(
    @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
    ErrorMessage = "Not a valid Id value"
)]
    public Guid Id { get; set; }
}
