using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.Register;

public class RegisterUserCommand : IRequest<bool>
{
    public required string Username { get; set; }
    
    public required string Password { get; set; }
    
    public required string Nickname { get; set; }
    
    public required string Email { get; set; }
}