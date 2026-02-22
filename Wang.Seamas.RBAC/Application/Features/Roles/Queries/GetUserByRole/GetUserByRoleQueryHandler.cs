using MediatR;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetUserByRole;

public class GetUserByRoleQueryHandler: IRequestHandler<GetUserByRoleQuery, List<UserDto>>
{
    private readonly IRoleService _roleService;

    public GetUserByRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<List<UserDto>> Handle(GetUserByRoleQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetUsersByRoleAsync(request.RoleId);
    }
}