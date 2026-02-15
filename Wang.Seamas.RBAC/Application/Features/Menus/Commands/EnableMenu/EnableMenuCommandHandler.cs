using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.EnableMenu;

public class EnableMenuCommandHandler : IRequestHandler<EnableMenuCommand, bool>
{
    private readonly IMenuService _menuService;

    public EnableMenuCommandHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<bool> Handle(EnableMenuCommand request, CancellationToken cancellationToken)
    {
        return await _menuService.EnableMenuAsync(request.Id, true);
    }
}