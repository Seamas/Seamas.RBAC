using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<bool>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
}