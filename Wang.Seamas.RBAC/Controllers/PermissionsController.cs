using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Permissions.Commands.SetRolePermission;
using Wang.Seamas.RBAC.Application.Features.Permissions.Commands.SetUserPermission;
using Wang.Seamas.RBAC.Application.Features.Permissions.Queries.UserVisibleMenus;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Attributes;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/permissions")]
[ControllerTag("权限管理")]
public class PermissionsController(IMediator mediator)
    : ControllerBase
{
    
    [HttpPost("set-role-permissions")]
    [ActionTag("设置角色权限")]
    public async Task<bool> SetRolePermissions(SetRolePermissionCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("set-user-permissions")]
    [ActionTag("设置用户权限")]
    public async Task<bool> SetUserPermissions(SetUserPermissionCommand request)
    {
        return await mediator.Send(request);
    }


    [HttpGet("get-user-visible-menus")]
    public async Task<List<Menu>> GetUserVisibleMenus()
    {
        var request = new UserVisibleMenusQuery();
        return await mediator.Send(request);
    }
    
}