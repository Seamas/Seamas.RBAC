using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetEndpointByRole;

public class GetEndpointByRoleQuery: IRequest<List<ApiEndpoint>>
{
    public int RoleId { get; set; }
}