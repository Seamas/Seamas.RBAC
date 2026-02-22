using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Permissions.Commands.SetUserPermission;

public class SetUserPermissionCommand : IRequest<bool>
{
    public int UserId { get; set; }
    
    public required List<int> MenuIds { get; set; }
    
    public required List<int> EndpointIds { get; set; }
}