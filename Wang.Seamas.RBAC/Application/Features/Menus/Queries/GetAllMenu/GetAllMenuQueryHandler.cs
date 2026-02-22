using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetAllMenu;

public class GetAllMenuQueryHandler : IRequestHandler<GetAllMenuQuery, List<Menu>>
{
    private readonly IMenuService  _menuService;

    public GetAllMenuQueryHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<List<Menu>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
    {
        return await _menuService.GetAllMenusAsync();
    }
}