using MediatR;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetUserPage;

public class GetUserPageQueryHandler : IRequestHandler<GetUserPageQuery, ResultPage<UserDto>>
{
    private readonly IUserService _userService;

    public GetUserPageQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ResultPage<UserDto>> Handle(GetUserPageQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await _userService.QueryUsersAsync(request);
        return new ResultPage<UserDto>(items, total, request.PageIndex, request.PageSize);
    }
}