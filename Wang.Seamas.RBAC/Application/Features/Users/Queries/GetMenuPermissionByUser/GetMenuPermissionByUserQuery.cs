using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetMenuPermissionByUser;

public class GetMenuPermissionByUserQuery : IRequest<List<Menu>>
{
    public int UserId { get; set; }
}