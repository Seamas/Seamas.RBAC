using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<bool>
{
    public required string Username { get; set; }
    
    public required string Nickname { get; set; }
    
    public required string Email { get; set; }
}