using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos;

public class LoginInput
{
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }
}
