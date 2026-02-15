using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest<bool>
{
    public int Id { get; set; }
}