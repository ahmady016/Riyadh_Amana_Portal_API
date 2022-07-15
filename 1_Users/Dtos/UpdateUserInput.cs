using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class UpdateUserInput : CreateUserInput
{
    [Required]
    public Guid Id { get; set; }
}
