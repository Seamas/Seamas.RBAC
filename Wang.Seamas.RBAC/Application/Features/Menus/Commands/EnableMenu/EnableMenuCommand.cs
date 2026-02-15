using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.EnableMenu;

public class EnableMenuCommand : IRequest<bool>
{
    public int Id { get; set; }
}