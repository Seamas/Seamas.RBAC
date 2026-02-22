using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetAllMenu;

public class GetAllMenuQuery: IRequest<List<Menu>>
{
    
}