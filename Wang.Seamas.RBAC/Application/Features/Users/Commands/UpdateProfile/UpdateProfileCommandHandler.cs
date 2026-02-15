using MediatR;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.Utils;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ProfileResponse>
{
    
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    
    public UpdateProfileCommandHandler(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }
    public async Task<ProfileResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var res = await _userService.UpdateUserProfileAsync(userId, request.Nickname, request.Email);
        
        Assert.IsTrue(res, "用户信息更新失败");
        var user = await _userService.GetUserByIdAsync(userId);
        return new ProfileResponse
        {
            Username = user!.Username, 
            Nickname = user.Nickname, 
            Email = user.Email
        };
    }
}