using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetApiByUser;

public class GetApiByUserQuery : IRequest<List<ApiEndpoint>>
{
    public int UserId { get; set; }
}