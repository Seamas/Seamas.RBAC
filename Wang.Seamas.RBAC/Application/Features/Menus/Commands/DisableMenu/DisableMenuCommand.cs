using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.DisableMenu;

public class DisableMenuCommand : IRequest<bool>
{
    public int Id { get; set; }
}