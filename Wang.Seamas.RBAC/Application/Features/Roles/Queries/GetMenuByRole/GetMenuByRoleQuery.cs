using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetMenuByRole;

public class GetMenuByRoleQuery: IRequest<List<Menu>>
{
    public int RoleId { get; set; }
}