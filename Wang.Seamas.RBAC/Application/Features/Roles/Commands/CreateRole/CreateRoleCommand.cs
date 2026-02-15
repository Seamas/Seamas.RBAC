using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommand: IRequest<bool>
{
    public required string Code { get; set; } 
    public required string Name { get; set; }
}