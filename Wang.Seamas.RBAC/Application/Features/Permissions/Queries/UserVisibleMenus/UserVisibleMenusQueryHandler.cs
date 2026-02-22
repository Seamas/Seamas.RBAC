using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Permissions.Queries.UserVisibleMenus;

public class UserVisibleMenusQueryHandler : IRequestHandler<UserVisibleMenusQuery, List<Menu>>
{
    private readonly IMenuService _menuService;
    private readonly ICurrentUserService _currentUserService;

    public UserVisibleMenusQueryHandler(IMenuService menuService, ICurrentUserService currentUserService)
    {
        _menuService = menuService;
        _currentUserService = currentUserService;
    }

    public async Task<List<Menu>> Handle(UserVisibleMenusQuery request, CancellationToken cancellationToken)
    {
        var userId = this._currentUserService.UserId;
        return await _menuService.GetUserVisibleMenusAsync(userId);
    }
}