using MediatR;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetUserByRole;

public class GetUserByRoleQuery : IRequest<List<UserDto>>
{
    public int RoleId { get; set; }
}