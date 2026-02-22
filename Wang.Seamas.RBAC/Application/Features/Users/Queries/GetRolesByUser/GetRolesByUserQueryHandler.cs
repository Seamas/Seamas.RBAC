using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetRolesByUser;

public class GetRolesByUserQueryHandler : IRequestHandler<GetRolesByUserQuery, List<Role>>
{
    private readonly IUserService _userService;

    public GetRolesByUserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<List<Role>> Handle(GetRolesByUserQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetRolesByUserIdAsync(request.UserId);
    }
}