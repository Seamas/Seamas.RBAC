using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.Web.Attributes;
using Wang.Seamas.RBAC.Requests.Auth;
using Wang.Seamas.RBAC.Responses;
using Wang.Seamas.RBAC.Responses.Auth;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Services.Model;
using Wang.Seamas.Web.Common.Utils;
using Wang.Seamas.Web.Services;
using Wang.Seamas.Web.Utils;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/auth")]
public class AuthController(IUserService userService, ITokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await userService.AuthenticateAsync(request.Username, request.Password);
        Assert.NotNull(user, "用户名或者密码错误");

        var authResult = new AuthResult
        {
            IsAuthenticated = true,
            UserId = user!.Id,
            Username = user.Username,
            Email = user.Email!
        };
        
        var token = tokenService.GenerateToken(authResult);
        return new LoginResponse
        (
            user.Id,
            user.Username,
            user.Nickname,
            token // 实际应生成 JWT
        );
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<int> CreateUser(RegisterUserRequest request)
    {
        var id = await userService.CreateUserAsync(
            request.Username,
            request.Password,
            request.Nickname,
            request.Email,
             true
        );
        return id;
    }

    [AllowedList]
    [HttpGet("profile")]
    public async Task<ProfileResponse> Profile()
    {
         var userId = HttpContextUtil.GetCurrentUserId(HttpContext);
        var user = await userService.GetUserByIdAsync(userId);
        
        Assert.NotNull(user, "当前用户不存在");
        
        return new ProfileResponse(user!.Username, user.Nickname, user.Email);
    }

    [AllowedList]
    [HttpPost("update-profile")]
    public async Task<ProfileResponse> UpdateProfile(UpdateProfileRequest request)
    {
        var userId = HttpContextUtil.GetCurrentUserId(HttpContext);
        var result = await userService.UpdateUserProfileAsync(userId, request.Nickname, request.Email);
        Assert.IsTrue(result, "用户信息更新失败");
        var user = await userService.GetUserByIdAsync(userId);
        return new ProfileResponse(user!.Username, user.Nickname, user.Email);
    }


    [AllowedList]
    [HttpPost("change-password")]
    public async Task<bool> ChangePassword(ChangePasswordRequest request)
    {
        var userId = HttpContextUtil.GetCurrentUserId(HttpContext);
        return await userService.ChangePasswordAsync(userId, request.OldPassword, request.NewPassword);
    }

    [AllowedList]
    [HttpPost("logout")]
    public Task<bool> Logout()
    {
        // TODO:
        return Task.FromResult(true);
    }
    
}