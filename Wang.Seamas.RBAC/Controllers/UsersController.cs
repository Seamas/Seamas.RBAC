using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Models.Dto;
using Wang.Seamas.RBAC.Requests;
using Wang.Seamas.RBAC.Requests.User;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Common.Dtos;
using Wang.Seamas.Web.Common.Utils;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/users")]
public class UsersController(
    IUserService userService,
    IUserRoleService userRoleService,
    IUserPermissionService userPermissionService)
    : ControllerBase
{
    private const string Password = "1@3$5^7*";
    
    [HttpPost("search")]
    public async Task<PagedResult<UserDto>> ListUsers(UserListRequest request)
    {
        var (users, total) = await userService.GetUsersAsync(
            request.PageIndex ?? 1,
            request.PageSize ?? 10,
            request.Username,
            request.Nickname,
            request.Email
        );
        return new PagedResult<UserDto>(users, total, request.PageIndex ?? 1, request.PageSize ?? 10);
    }

    [HttpPost("create")]
    public async Task<bool> CreateUser(CreateUserRequest request)
    {
        var result = await userService.CreateUserAsync(
            request.Username,
            Password,
            request.Nickname,
            request.Email,
            true
        );
        return result > 0;
    }

    [HttpPost("update")]
    public async Task<bool> UpdateUser(UpdateUserRequest request)
    {
        return await userService.UpdateUserProfileAsync(request.Id, request.Nickname, request.Email);
    }
    

    [HttpPost("get")]
    public async Task<UserDto?> GetUser(GetUserRequest request)
    {
        var user = await userService.GetUserByIdAsync(request.Id);
        Assert.NotNull(user, "User not found");
        return user;
    }

    [HttpPost("enable")]
    public async Task<bool> SetEnabled(EnableUserRequest request)
    {
        return await userService.EnableUserAsync(request.Id, request.Enabled);
    }

    [HttpPost("reset-password")]
    public async Task<bool> ResetPassword(GetUserRequest request)
    {
        return await userService.ResetPasswordAsync(request.Id, Password);
    }
    

    [HttpPost("assign-roles")]
    public async Task<bool> AssignRoles(AssignRolesRequest request)
    {
        await userRoleService.AssignRolesToUserAsync(request.UserId, request.RoleIds);
        return true;
    }
    
    [HttpPost("set-menu-permissions")]
    public async Task<ApiResult> SetMenuPermissions(SetUserMenuPermissionsRequest request)
    {
        await userPermissionService.SetUserMenuPermissionsAsync(request.UserId, request.Permissions);
        return ApiResult.Ok();
    }

    [HttpPost("check-username")]
    public async Task<bool> CheckUsername(UsernameRequest request)
    {
        return await userService.CheckUsernameAsync(request.Username);
    }
}