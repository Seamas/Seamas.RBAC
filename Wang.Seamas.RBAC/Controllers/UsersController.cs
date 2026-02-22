using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.AssignRole;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.CreateUser;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.DisableUser;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.EnableUser;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.ResetPassword;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.UpdateUser;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.CheckUsername;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetAllUsers;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetApiByUser;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetApiPermissionByUser;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetMenuByUser;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetMenuPermissionByUser;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetRolesByUser;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetUser;
using Wang.Seamas.RBAC.Application.Features.Users.Queries.GetUserPage;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Attributes;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/users")]
[ControllerTag("用户管理")]
public class UsersController(IMediator mediator)
    : ControllerBase
{
    
    [HttpPost("search")]
    [ActionTag("查询")]
    public async Task<ResultPage<UserDto>> ListUsers(GetUserPageQuery request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("create")]
    [ActionTag("创建用户")]
    public async Task<bool> CreateUser(CreateUserCommand command)
    {
        return await mediator.Send(command);
    }

    [HttpPost("update")]
    [ActionTag("更新用户")]
    public async Task<bool> UpdateUser(UpdateUserCommand request)
    {
        return await mediator.Send(request);
    }
    

    [HttpPost("get")]
    [ActionTag("获取单个用户信息")]
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
    [ActionTag("启用用户")]
    public async Task<bool> SetEnabled(EnableUserCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("disable")]
    [ActionTag("禁用用户")]
    public async Task<bool> SetDisabled(DisableUserCommand request)
    {
        return await mediator.Send(request);
    }

    #endregion
    
    
    

    [HttpPost("reset-password")]
    [ActionTag("重置密码")]
    public async Task<bool> ResetPassword(ResetPasswordCommand command)
    {
        return await mediator.Send(command);
    }
    

    [HttpPost("assign-roles")]
    [ActionTag("将用户授予多个角色")]
    public async Task<bool> AssignRoles(AssignRoleCommand request)
    {
        return await mediator.Send(request);
    }
    

    [AllowAnonymous]
    [HttpPost("check-username")]
    [ActionTag("检查用户名")]
    public async Task<bool> CheckUsername(CheckUsernameCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpGet("all-users")]
    [ActionTag("获取所有用户")]
    public async Task<List<UserDto>> GetAllUsers()
    {
        return await mediator.Send(new GetAllUsersQuery());
    }
    
    
    [HttpPost("get-roles-by-user")]
    [ActionTag("根据用户获取相应的角色")]
    public async Task<IEnumerable<Role>> GetRolesByUserId(GetRolesByUserQuery request)
    {
        return await mediator.Send(request);
    }
    
    [HttpPost("get-menus-by-user")]
    [ActionTag("根据用户获取相应菜单")]
    public async Task<IEnumerable<Menu>> GetMenusByUser(GetMenuByUserQuery request)
    {
        return await mediator.Send(request);
    }
    
    
    [HttpPost("get-menu-permissions-by-user")]
    [ActionTag("根据用户获取覆盖的菜单权限")]
    public async Task<IEnumerable<Menu>> GetMenusPermissionsByUser(GetMenuPermissionByUserQuery request)
    {
        return await mediator.Send(request);
    }
    
    [HttpPost("get-apis-by-user")]
    [ActionTag("根据用户获取相应接口")]
    public async Task<IEnumerable<ApiEndpoint>> GetApisByUser(GetApiByUserQuery request)
    {
        return await mediator.Send(request);
    }

    
    
    [HttpPost("get-api-permissions-by-user")]
    [ActionTag("根据用户获取覆盖的接口权限")]
    public async Task<IEnumerable<ApiEndpoint>> GetApisPermissionsByUser(GetApiPermissionByUserQuery request)
    {
        return await mediator.Send(request);
    }
}