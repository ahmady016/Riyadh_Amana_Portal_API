using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos;

public class RefreshTokenInput
{
    [Required(ErrorMessage = "Token is Required")]
    [StringLength(88, MinimumLength = 88, ErrorMessage = "Token string must be 88 characters")]
    public string Token { get; set; }
}
