using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.ActiveRole;

public class ActiveRoleQuery: IRequest<IEnumerable<Role>>
{
    
}