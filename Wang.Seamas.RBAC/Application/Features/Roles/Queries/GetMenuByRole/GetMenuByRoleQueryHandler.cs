using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetMenuByRole;

public class GetMenuByRoleQueryHandler: IRequestHandler<GetMenuByRoleQuery, List<Menu>>
{
    private readonly IMenuService _menuService;

    public GetMenuByRoleQueryHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<List<Menu>> Handle(GetMenuByRoleQuery request, CancellationToken cancellationToken)
    {
        return await _menuService.GetMenusByRoleIdAsync(request.RoleId);
    }
}