using Dtos;

namespace Auth.Dtos;

public class AuthDto
{
    public UserDto User { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
