using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetMenu;

public class GetMenuQuery: IRequest<Menu>
{
    public int Id { get; set; }
}