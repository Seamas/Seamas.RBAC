using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Queries.FirstLevelMenu;

public class FirstLevelMenuQueryHandler : IRequestHandler<FirstLevelMenuQuery, IEnumerable<Menu>>
{
    private readonly IMenuService _menuService;

    public FirstLevelMenuQueryHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<IEnumerable<Menu>> Handle(FirstLevelMenuQuery request, CancellationToken cancellationToken)
    {
        return await _menuService.GetFirstLevelMenusAsync();
    }
}