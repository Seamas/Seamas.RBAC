using MediatR;
using SqlSugar;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.AssignRole;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
{
    private readonly IUserRoleService _userRoleService;
    private readonly ISqlSugarClient _db;
    private readonly IUserPermissionService _userPermissionService;


    public AssignRoleCommandHandler(IUserRoleService userRoleService, ISqlSugarClient db, IUserPermissionService userPermissionService)
    {
        _userRoleService = userRoleService;
        _db = db;
        _userPermissionService = userPermissionService;
    }

    public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var res = await _db.Ado.UseTranAsync(async () =>
        {
            await _userRoleService.AssignRolesToUserAsync(request.UserId, request.RoleIds);
            await _userPermissionService.RemoveDeprecatedUserApiPermissionAsync(request.UserId);
            await _userPermissionService.RemoveDeprecatedUserMenuPermissionAsync(request.UserId);
        });
        return res.IsSuccess;
    }
}