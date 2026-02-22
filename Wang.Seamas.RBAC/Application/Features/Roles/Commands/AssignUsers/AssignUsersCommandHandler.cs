using MediatR;
using SqlSugar;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.AssignUsers;

public class AssignUsersCommandHandler : IRequestHandler<AssignUsersCommand, bool>
{
    private readonly IRoleService _roleService;
    private readonly ISqlSugarClient _db;
    private readonly IUserPermissionService _userPermissionService;

    public AssignUsersCommandHandler(IRoleService roleService, ISqlSugarClient db, IUserPermissionService userPermissionService)
    {
        _roleService = roleService;
        _db = db;
        _userPermissionService = userPermissionService;
    }

    public async Task<bool> Handle(AssignUsersCommand request, CancellationToken cancellationToken)
    {
        var res = await _db.Ado.UseTranAsync(async () =>
        {
            await _roleService.AssignUsersAsync(request.RoleId, request.UserIds);
            
            await _userPermissionService.RemoveDeprecatedApiPermissionAsync();
            await _userPermissionService.RemoveDeprecatedApiPermissionAsync();
        });
        return res.IsSuccess;
    }
}