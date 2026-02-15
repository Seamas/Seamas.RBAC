using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.DeleteMenu;

public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, bool>
{
    private readonly IMenuService _menuService;

    public DeleteMenuCommandHandler(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<bool> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
    {
        return await _menuService.DeleteMenuAsync(request.Id);
    }
}