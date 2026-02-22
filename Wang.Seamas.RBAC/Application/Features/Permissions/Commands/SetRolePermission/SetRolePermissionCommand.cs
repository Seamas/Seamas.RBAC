using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Permissions.Commands.SetRolePermission;

public class SetRolePermissionCommand : IRequest<bool>
{
    public int RoleId { get; set; }
    
    public required List<int> MenuIds { get; set; }
    
    public required List<int> EndpointIds { get; set; }
}