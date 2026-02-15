using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Role?>
{
    private readonly IRoleService _roleService;

    public GetRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<Role?> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetRoleByIdAsync(request.Id);
    }
}