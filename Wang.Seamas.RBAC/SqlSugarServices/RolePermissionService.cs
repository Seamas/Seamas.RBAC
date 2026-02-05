using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;

namespace Wang.Seamas.RBAC.SqlSugarServices;

public class RolePermissionService(ISqlSugarClient db) : IRolePermissionService
{
    public async Task SetRoleMenuPermissionsAsync(int roleId, List<int> menuIds)
    {
        await db.Ado.UseTranAsync(async () =>
        {
            await db.Deleteable<RoleMenuPermission>().Where( x => x.RoleId == roleId).ExecuteCommandAsync();
            if (menuIds?.Any() == true)
            {
                var data = menuIds.Select(m => new { role_id = roleId, menu_id = m }).ToList();
                await db.Insertable<RoleMenuPermission>(data).ExecuteCommandAsync();
            }
        });
    }

    public async Task SetRoleApiPermissionsAsync(int roleId, List<int> apiEndpointIds)
    {
        await db.Ado.UseTranAsync(async () =>
        {
            await db.Deleteable<RoleApiPermission>().Where(x => x.RoleId == roleId).ExecuteCommandAsync();
            if (apiEndpointIds?.Any() == true)
            {
                var data = apiEndpointIds.Select(id => new { role_id = roleId, api_endpoint_id = id }).ToList();
                await db.Insertable<RoleApiPermission>(data).ExecuteCommandAsync();
            }
        });
    }

    public async Task<List<int>> GetMenuIdsByRoleIdAsync(int roleId)
        => await db.Queryable<RoleMenuPermission>().Where(x => x.RoleId == roleId).Select(x => x.MenuId).ToListAsync();

    public async Task<List<int>> GetApiEndpointIdsByRoleIdAsync(int roleId)
        => await db.Queryable<RoleApiPermission>().Where(x => x.RoleId == roleId).Select(x => x.ApiEndpointId).ToListAsync();
}