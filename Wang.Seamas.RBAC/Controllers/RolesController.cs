using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.CreateRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.DeleteRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.DisableRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.EnableRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Commands.UpdateRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.ActiveRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.CheckCode;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetRole;
using Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetRolePage;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.RBAC.Requests;
using Wang.Seamas.Web.Common.DTOs;
using Wang.Seamas.Web.Common.Utils;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/roles")]
public class RolesController(IRolePermissionService rolePermissionService, IMediator mediator)
    : ControllerBase
{

    [HttpGet("list")]
    public async Task<IEnumerable<Role>> ListRoles()
    {
        var request = new ActiveRoleQuery();
        return await mediator.Send(request);
    }
    
    [HttpPost("search")]
    public async Task<ResultPage<Role>> QueryRoles(GetRolePageQuery request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("create")]
    public async Task<bool> CreateRole(CreateRoleCommand request) 
        => await mediator.Send(request);

    [HttpPost("get")]
    public async Task<Role?> GetRole(GetRoleQuery request) => await mediator.Send(request);

    
    [HttpPost("update")]
    public async Task<bool> UpdateRole(UpdateRoleCommand request)
    {
       return await  mediator.Send(request);
    }

    [HttpPost("delete")]
    public async Task<bool> DeleteRole(DeleteRoleCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("enable")]
    public async Task<bool> EnableRole(EnableRoleCommand request)
        => await mediator.Send(request);
    
    
    [HttpPost("disable")]
    public async Task<bool> DisableRole(DisableRoleCommand request) 
        => await mediator.Send(request);


    [HttpPost("check-code")]
    public async Task<bool> CheckRoleCode(CheckCodeQuery checkRequest) =>
        await mediator.Send(checkRequest);

    [HttpPost("set-menu-permissions")]
    public async Task<bool> SetMenuPermissions(SetRoleMenuPermissionsRequest request)
    {
        await rolePermissionService.SetRoleMenuPermissionsAsync(request.RoleId, request.MenuIds);
        return  true;
    }
    
}