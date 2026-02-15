using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.ActiveRole;

public class ActiveRoleQueryHandler : IRequestHandler<ActiveRoleQuery, IEnumerable<Role>>
{
    private readonly IRoleService _roleService;

    public ActiveRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IEnumerable<Role>> Handle(ActiveRoleQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetActiveRolesAsync();
    }
}