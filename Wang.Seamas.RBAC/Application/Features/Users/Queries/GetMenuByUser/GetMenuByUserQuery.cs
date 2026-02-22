using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetMenuByUser;

public class GetMenuByUserQuery : IRequest<List<Menu>>
{
    public int UserId { get; set; }
}