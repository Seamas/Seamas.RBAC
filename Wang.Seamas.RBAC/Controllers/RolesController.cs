using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Requests;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Common;
using Wang.Seamas.Web.Common.Utils;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/roles")]
public class RolesController(IRoleService roleService, IRolePermissionService rolePermissionService)
    : ControllerBase
{
    [HttpPost("list")]
    public async Task<List<Role>> ListRoles()
        => await roleService.GetActiveRolesAsync();

    [HttpPost("create")]
    public async Task<int> CreateRole(CreateRoleRequest request) 
        => await roleService.CreateRoleAsync(request.Name, request.IsEnabled ?? true);

    [HttpPost("get")]
    public async Task<Role?> GetRole(GetRoleRequest request) => await roleService.GetRoleByIdAsync(request.Id);

    
    [HttpPost("update")]
    public async Task<bool> UpdateRole(UpdateRoleRequest request)
    {
        var success = await roleService.UpdateRoleAsync(
            request.Id,
            request.Name,
            request.IsEnabled
        );
        Assert.IsTrue(success, "Role not found");
        return success;
    }

    [HttpPost("set-menu-permissions")]
    public async Task<bool> SetMenuPermissions(SetRoleMenuPermissionsRequest request)
    {
        await rolePermissionService.SetRoleMenuPermissionsAsync(request.RoleId, request.MenuIds);
        return  true;
    }
    
}