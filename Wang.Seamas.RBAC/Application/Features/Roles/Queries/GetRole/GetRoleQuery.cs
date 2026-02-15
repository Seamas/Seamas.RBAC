using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetRole;

public class GetRoleQuery: IRequest<Role?>
{
    public int Id { get; set; }
}