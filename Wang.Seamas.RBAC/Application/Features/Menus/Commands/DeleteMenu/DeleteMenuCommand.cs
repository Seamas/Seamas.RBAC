using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.DeleteMenu;

public class DeleteMenuCommand: IRequest<bool>
{
    public int Id { get; set; }
}