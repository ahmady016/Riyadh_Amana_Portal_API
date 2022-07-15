using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class UpdateAdvertisementInput : CreateAdvertisementInput
{
    [Required]
    public Guid Id { get; set; }
}
