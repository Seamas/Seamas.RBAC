using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetApiPermissionByUser;

public class GetApiPermissionByUserQuery : IRequest<List<ApiEndpoint>>
{
    public int UserId { get; set; }
}