using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Requests;
using Wang.Seamas.RBAC.Requests.Role;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Common.Dtos;
using Wang.Seamas.Web.Common.Utils;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/roles")]
public class RolesController(IRoleService roleService, IRolePermissionService rolePermissionService)
    : ControllerBase
{

    [HttpGet("list")]
    public async Task<List<Role>> ListRoles() => await roleService.GetActiveRolesAsync();
    
    [HttpPost("search")]
    public async Task<PagedResult<Role>> QueryRoles(SearchRoleRequest request)
    {
        var (list, totalCount) = await roleService.GetRolesAsync(
            request.PageIndex ?? 1, 
            request.PageSize ?? 10, 
            request.Code, 
            request.Name);
        return new PagedResult<Role>(list, totalCount, request.PageIndex ?? 1, request.PageSize ?? 10);
    }

    [HttpPost("create")]
    public async Task<int> CreateRole(CreateRoleRequest request) 
        => await roleService.CreateRoleAsync(request.Code, request.Name, request.IsEnabled ?? true);

    [HttpPost("get")]
    public async Task<Role?> GetRole(RoleIdRequest idRequest) => await roleService.GetRoleByIdAsync(idRequest.Id);

    
    [HttpPost("update")]
    public async Task<bool> UpdateRole(UpdateRoleRequest request)
    {
        var success = await roleService.UpdateRoleAsync(
            request.Id,
            request.Code,
            request.Name
        );
        Assert.IsTrue(success, "Role not found");
        return success;
    }

    [HttpPost("delete")]
    public async Task<bool> DeleteRole(RoleIdRequest request)
    {
        var success = await roleService.DeleteRoleAsync(request.Id);
        Assert.IsTrue(success, "Role not found");
        return success;
    }

    [HttpPost("enable")]
    public async Task<bool> EnableRole(EnableRoleRequest request) 
        => await roleService.SetRoleEnabledAsync(request.Id, request.Enabled);


    [HttpPost("check-code")]
    public async Task<bool> CheckRoleCode(RoleCodeCheckRequest checkRequest) =>
        await roleService.CheckCodeAsync(checkRequest.Id, checkRequest.Code);

    [HttpPost("set-menu-permissions")]
    public async Task<bool> SetMenuPermissions(SetRoleMenuPermissionsRequest request)
    {
        await rolePermissionService.SetRoleMenuPermissionsAsync(request.RoleId, request.MenuIds);
        return  true;
    }
    
}