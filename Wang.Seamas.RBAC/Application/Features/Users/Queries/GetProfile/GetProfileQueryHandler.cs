using MediatR;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.Utils;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileResponse>
{
    
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public GetProfileQueryHandler(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }
    public async Task<ProfileResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var user = await _userService.GetUserByIdAsync(userId);
        
        Assert.NotNull(user, "当前用户不存在");

        return new ProfileResponse
        {
            Username = user.Username,
            Email = user.Email,
            Nickname = user.Nickname,
        };
    }
}