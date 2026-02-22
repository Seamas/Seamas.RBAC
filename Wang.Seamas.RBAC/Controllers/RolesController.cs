using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.AssignUsers;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.CreateRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.DeleteRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.DisableRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.EnableRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.UpdateRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.ActiveRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.CheckCode;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetEndpointByRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetMenuByRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetRolePage;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetUserByRole;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Attributes;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/roles")]
[ControllerTag("角色管理")]
public class RolesController(IMediator mediator)
    : ControllerBase
{

    [HttpGet("list")]
    [ActionTag("列出所有角色")]
    public async Task<IEnumerable<Role>> ListRoles()
    {
        var request = new ActiveRoleQuery();
        return await mediator.Send(request);
    }
    
    [HttpPost("search")]
    [ActionTag("查询")]
    public async Task<ResultPage<Role>> QueryRoles(GetRolePageQuery request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("create")]
    [ActionTag("创建角色")]
    public async Task<bool> CreateRole(CreateRoleCommand request) 
        => await mediator.Send(request);

    [HttpPost("get")]
    [ActionTag("获取单个角色")]
    public async Task<Role?> GetRole(GetRoleQuery request) => await mediator.Send(request);

    
    [HttpPost("update")]
    [ActionTag("更新角色")]
    public async Task<bool> UpdateRole(UpdateRoleCommand request)
    {
       return await  mediator.Send(request);
    }

    [HttpPost("delete")]
    [ActionTag("删除角色")]
    public async Task<bool> DeleteRole(DeleteRoleCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("enable")]
    [ActionTag("启用角色")]
    public async Task<bool> EnableRole(EnableRoleCommand request)
        => await mediator.Send(request);
    
    
    [HttpPost("disable")]
    [ActionTag("禁用角色")]
    public async Task<bool> DisableRole(DisableRoleCommand request) 
        => await mediator.Send(request);


    [HttpPost("check-code")]
    [ActionTag("检查编码")]
    public async Task<bool> CheckRoleCode(CheckCodeQuery checkRequest) =>
        await mediator.Send(checkRequest);
    

    [HttpPost("get-users-by-role")]
    [ActionTag("根据角色获取用户列表")]
    public async Task<IEnumerable<UserDto>> GetUsersByRole(GetUserByRoleQuery request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("assign-users-to-role")]
    [ActionTag("将多个用户授权一个角色")]
    public async Task<bool> AssignUsers(AssignUsersCommand request)
    {
        return await mediator.Send(request);
    }
    
    [HttpPost("get-menus-by-role")]
    [ActionTag("根据角色获取对应的菜单")]
    public async Task<IEnumerable<Menu>> GetMenusByRoleId(GetMenuByRoleQuery request)
    {
        return await mediator.Send(request);
    }
    
    [HttpPost("get-endpoints-by-role")]
    [ActionTag("根据角色获取对应的接口")]
    public async Task<IEnumerable<ApiEndpoint>> GetEndpointsByRole(GetEndpointByRoleQuery request) => await mediator.Send(request);
}