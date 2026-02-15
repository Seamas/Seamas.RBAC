using MediatR;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetUser;

public class GetUserQuery : IRequest<UserDto>
{
    public int Id { get; set; }
}