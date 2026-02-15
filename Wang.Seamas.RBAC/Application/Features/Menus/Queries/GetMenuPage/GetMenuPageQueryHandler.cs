using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetMenuPage;

public class GetMenuPageQueryHandler : IRequestHandler<GetMenuPageQuery, ResultPage<Menu>>
{
    
    private readonly IMenuService _menuService;

    public GetMenuPageQueryHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }


    public async Task<ResultPage<Menu>> Handle(GetMenuPageQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await _menuService.QueryMenusAsync(request);
        return new ResultPage<Menu>(items, total, request.PageIndex, request.PageSize);
    }
}