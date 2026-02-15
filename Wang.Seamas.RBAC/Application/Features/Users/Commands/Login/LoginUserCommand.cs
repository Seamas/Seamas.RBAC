using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.Login;

public class LoginUserCommand: IRequest<LoginUserResponse>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}