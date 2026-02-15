using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.Utils;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetMenu;

public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, Menu>
{
    private readonly IMenuService _menuService;

    public GetMenuQueryHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<Menu> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var result = await _menuService.GetMenuByIdAsync(request.Id);
        Assert.NotNull(result, $"菜单{request.Id}不存在");
        return result!;
    }
}