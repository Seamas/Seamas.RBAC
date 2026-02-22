using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.ChangePassword;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.Login;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.Logout;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.Register;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.UpdateProfile;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetProfile;
using Wang.Seamas.Web.Attributes;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/auth")]
[ControllerTag("个人管理")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ActionTag("登录")]
    public async Task<LoginUserResponse> Login(LoginUserCommand request)
    {
        return await mediator.Send(request);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    [ActionTag("注册")]
    public async Task<bool> CreateUser(RegisterUserCommand command)
    {
        return await mediator.Send(command);
    }

    [AllowedList]
    [HttpGet("profile")]
    [ActionTag("查看个人资料")]
    public async Task<ProfileResponse> Profile()
    {
        var request = new GetProfileQuery();
        return await mediator.Send(request);
    }

    [AllowedList]
    [HttpPost("update-profile")]
    [ActionTag("更新个人资料")]
    public async Task<ProfileResponse> UpdateProfile(UpdateProfileCommand request)
    {
        return await mediator.Send(request);
    }


    [AllowedList]
    [HttpPost("change-password")]
    [ActionTag("修改密码")]
    public async Task<bool> ChangePassword(ChangePasswordCommand request)
    {
        return await mediator.Send(request);
    }

    [AllowedList]
    [HttpPost("logout")]
    [ActionTag("退出系统")]
    public async Task<bool> Logout()
    {
        var request = new LogoutUserCommand();
        return await mediator.Send(request);
    }
    
}