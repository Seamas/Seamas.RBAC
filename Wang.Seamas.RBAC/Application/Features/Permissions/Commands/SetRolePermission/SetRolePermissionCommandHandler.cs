using MediatR;
using SqlSugar;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Permissions.Commands.SetRolePermission;

public class SetRolePermissionCommandHandler : IRequestHandler<SetRolePermissionCommand, bool>
{
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IUserPermissionService _userPermissionService;
    private readonly ISqlSugarClient _db;

    public SetRolePermissionCommandHandler(IRolePermissionService rolePermissionService, ISqlSugarClient db, IUserPermissionService userPermissionService)
    {
        _rolePermissionService = rolePermissionService;
        _db = db;
        _userPermissionService = userPermissionService;
    }

    public async Task<bool> Handle(SetRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var res = await _db.Ado.UseTranAsync(async () =>
        {
            await _rolePermissionService.SetRoleMenuPermissionsAsync(request.RoleId, request.MenuIds);
            await _rolePermissionService.SetRoleApiPermissionsAsync(request.RoleId, request.EndpointIds);
            await _userPermissionService.RemoveDeprecatedApiPermissionAsync();
            await _userPermissionService.RemoveDeprecatedMenuPermissionAsync();

        });

        return res.IsSuccess;
    }
}