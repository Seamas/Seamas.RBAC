using MediatR;
using SqlSugar;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Permissions.Commands.SetUserPermission;

public class SetUserPermissionCommandHandler : IRequestHandler<SetUserPermissionCommand, bool>
{
    private readonly ISqlSugarClient _db;
    private readonly IUserPermissionService _userPermissionService;

    public SetUserPermissionCommandHandler(ISqlSugarClient db, IUserPermissionService userPermissionService)
    {
        _db = db;
        _userPermissionService = userPermissionService;
    }

    public async Task<bool> Handle(SetUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var res = await _db.Ado.UseTranAsync(async () =>
        {
            await _userPermissionService.SetUserApiPermissionsAsync(request.UserId, request.EndpointIds);
            await _userPermissionService.SetUserMenuPermissionsAsync(request.UserId, request.MenuIds);
            
            //
            await _userPermissionService.RemoveDeprecatedUserMenuPermissionAsync(request.UserId);
            await _userPermissionService.RemoveDeprecatedUserApiPermissionAsync(request.UserId);
            
        });
        return res.IsSuccess;
    }
}