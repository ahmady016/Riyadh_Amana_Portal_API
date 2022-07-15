using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class UpdateNewsInput
{
    [Required]
    public Guid Id { get; set; }
}
