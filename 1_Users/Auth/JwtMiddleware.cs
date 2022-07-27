using Users;

namespace Auth;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!String.IsNullOrEmpty(token))
        {
            var userId = AuthHelpers.ValidateTokenAndGetUserId(token);
            context.Items["User"] = userService.Find(userId);
        }
        await _next(context);
    }
}
