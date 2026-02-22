using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetMenuByUser;

public class GetMenuByUserQueryHandler: IRequestHandler<GetMenuByUserQuery, List<Menu>>
{
    private readonly IMenuService _menuService;

    public GetMenuByUserQueryHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<List<Menu>> Handle(GetMenuByUserQuery request, CancellationToken cancellationToken)
    {
        return await _menuService.GetMenusByUserIdAsync(request.UserId);
    }
}