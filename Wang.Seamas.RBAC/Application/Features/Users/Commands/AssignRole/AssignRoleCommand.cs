using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.AssignRole;

public class AssignRoleCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public required List<int> RoleIds { get; set; }
}