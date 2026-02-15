using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.CreateUser;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.DisableUser;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.EnableUser;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.ResetPassword;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.UpdateUser;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.CheckUsername;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetUser;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetUserPage;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.RBAC.Requests;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/users")]
public class UsersController(
    IUserRoleService userRoleService,
    IUserPermissionService userPermissionService,
    IMediator mediator)
    : ControllerBase
{

    
    [HttpPost("search")]
    public async Task<ResultPage<UserDto>> ListUsers(GetUserPageQuery request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("create")]
    public async Task<bool> CreateUser(CreateUserCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpPost("update")]
    public async Task<bool> UpdateUser(UpdateUserCommand request)
    {
        return await mediator.Send(request);
    }
    

    [HttpPost("get")]
    public async Task<UserDto?> GetUser(GetUserQuery request)
    {
        return await mediator.Send(request);
    }

    #region  启用/禁用

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost("enable")]
    public async Task<bool> SetEnabled(EnableUserCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("disable")]
    public async Task<bool> SetDisabled(DisableUserCommand request)
    {
        return await mediator.Send(request);
    }

    #endregion
    
    
    

    [HttpPost("reset-password")]
    public async Task<bool> ResetPassword(ResetPasswordCommand command)
    {
        return await mediator.Send(command);
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

    [AllowAnonymous]
    [HttpPost("check-username")]
    public async Task<bool> CheckUsername(CheckUsernameCommand request)
    {
        return await mediator.Send(request);
    }
}