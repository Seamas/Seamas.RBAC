using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.EnableRole;

public class EnableRoleCommand: IRequest<bool>
{
    public int Id { get; set; }
}