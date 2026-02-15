using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.DisableRole;

public class DisableRoleCommand : IRequest<bool>
{
    public int Id { get; set; }
}