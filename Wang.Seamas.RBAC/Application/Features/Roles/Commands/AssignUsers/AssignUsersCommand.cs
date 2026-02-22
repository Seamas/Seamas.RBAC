using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.AssignUsers;

public class AssignUsersCommand : IRequest<bool>
{
    public int RoleId { get; set; }
    public required List<int> UserIds { get; set; }
}