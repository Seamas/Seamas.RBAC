using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.DisableMenu;

public class DisableMenuCommandHandler : IRequestHandler<DisableMenuCommand, bool>
{
    private readonly IMenuService _menuService;

    public DisableMenuCommandHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<bool> Handle(DisableMenuCommand request, CancellationToken cancellationToken)
    {
        return await _menuService.EnableMenuAsync(request.Id, false);
    }
}