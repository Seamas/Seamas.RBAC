using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetMenuPermissionByUser;

public class GetMenuPermissionsByUserQueryHandler: IRequestHandler<GetMenuPermissionByUserQuery, List<Menu>>
{
    private readonly IMenuService _menuService;

    public GetMenuPermissionsByUserQueryHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<List<Menu>> Handle(GetMenuPermissionByUserQuery request, CancellationToken cancellationToken)
    {
        return await _menuService.GetMenuPermissionsByUserIdAsync(request.UserId);
    }
}