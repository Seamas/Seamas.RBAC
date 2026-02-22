using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.ActiveRole;

public class ActiveRoleQueryHandler : IRequestHandler<ActiveRoleQuery, List<Role>>
{
    private readonly IRoleService _roleService;

    public ActiveRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<List<Role>> Handle(ActiveRoleQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetActiveRolesAsync();
    }
}