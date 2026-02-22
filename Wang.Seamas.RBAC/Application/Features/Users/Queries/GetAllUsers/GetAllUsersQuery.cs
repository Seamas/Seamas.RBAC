using MediatR;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<List<UserDto>>
{
    
}