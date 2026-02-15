using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<bool>
{
    public int Id { get; set; } 
    public string? Nickname { get; set; }
    public string? Email { get; set; }
}