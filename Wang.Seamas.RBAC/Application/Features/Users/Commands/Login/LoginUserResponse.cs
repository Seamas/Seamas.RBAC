namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.Login;

public class LoginUserResponse
{
    public required string Username { get; set; }
    public required string Nickname { get; set; }
    public required string Token { get; set; }
}