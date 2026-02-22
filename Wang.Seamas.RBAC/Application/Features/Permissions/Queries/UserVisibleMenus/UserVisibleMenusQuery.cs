using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Permissions.Queries.UserVisibleMenus;

public class UserVisibleMenusQuery : IRequest<List<Menu>>
{
}