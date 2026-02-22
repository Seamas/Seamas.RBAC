using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetRolesByUser;

public class GetRolesByUserQuery: IRequest<List<Role>>
{
    public int UserId { get; set; }
}