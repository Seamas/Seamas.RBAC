using SqlSugar;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Services;

public class RolePermissionService(ISqlSugarClient db) : IRolePermissionService
{
    public async Task SetRoleMenuPermissionsAsync(int roleId, List<int> menuIds)
    {
        await db.Deleteable<RoleMenuPermission>().Where( x => x.RoleId == roleId).ExecuteCommandAsync();
        var list = menuIds.ToList();
        if (list.Count > 0)
        {
            var data = list.Select(m => new RoleMenuPermission { RoleId = roleId, MenuId = m }).ToList();
            await db.Insertable<RoleMenuPermission>(data).ExecuteCommandAsync();
        }
    }

    public async Task SetRoleApiPermissionsAsync(int roleId, List<int> apiEndpointIds)
    {
        await db.Deleteable<RoleApiPermission>().Where(x => x.RoleId == roleId).ExecuteCommandAsync();
        var list = apiEndpointIds.ToList();
        if (list.Count > 0)
        {
            var data = list.Select(m => new RoleApiPermission{ RoleId = roleId, ApiEndpointId = m }).ToList();
            await db.Insertable<RoleApiPermission>(data).ExecuteCommandAsync();
        }
    }

    public async Task<List<int>> GetMenuIdsByRoleIdAsync(int roleId)
        => await db.Queryable<RoleMenuPermission>().Where(x => x.RoleId == roleId).Select(x => x.MenuId).ToListAsync();

    public async Task<List<int>> GetApiEndpointIdsByRoleIdAsync(int roleId)
        => await db.Queryable<RoleApiPermission>().Where(x => x.RoleId == roleId).Select(x => x.ApiEndpointId).ToListAsync();
}