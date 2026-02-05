using SqlSugar;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;

namespace Wang.Seamas.RBAC.SqlSugarServices;

public class UserPermissionService(ISqlSugarClient db) : IUserPermissionService
{
    public async Task SetUserMenuPermissionAsync(int userId, int menuId, bool isAllowed)
    {
        var firstAsync = await db.Queryable<UserMenuPermission>().Where(x => x.UserId == userId && x.MenuId == menuId)
            .FirstAsync();
        if (firstAsync != null)
        {
            await db.Updateable<UserMenuPermission>()
                .SetColumns(x => new UserMenuPermission { IsAllowed = isAllowed })
                .ExecuteCommandAsync();
        }
        else
        {
            await db.Insertable<UserMenuPermission>(new { UserId = userId, MenuId = menuId, IsAllowed = isAllowed }).ExecuteCommandAsync();
        }
    }

    public async Task SetUserMenuPermissionsAsync(int userId, Dictionary<int, bool> permissions)
    {
        foreach (var (menuId, allowed) in permissions)
            await SetUserMenuPermissionAsync(userId, menuId, allowed);
    }

    public async Task SetUserApiPermissionAsync(int userId, int apiEndpointId, bool isAllowed)
    {
        var firstAsync = await db.Queryable<UserApiPermission>().Where(x => x.UserId == userId && x.ApiEndpointId == apiEndpointId)
            .FirstAsync();
        if (firstAsync != null)
        {
            await db.Updateable<UserApiPermission>()
                .SetColumns(x => new UserApiPermission { IsAllowed = isAllowed })
                .ExecuteCommandAsync();
        }
        else
        {
            await db.Insertable<UserApiPermission>(new { UserId = userId, ApiEndpointId = apiEndpointId, IsAllowed = isAllowed }).ExecuteCommandAsync();
        }
    }

    public async Task SetUserApiPermissionsAsync(int userId, Dictionary<int, bool> permissions)
    {
        foreach (var (apiId, allowed) in permissions)
            await SetUserApiPermissionAsync(userId, apiId, allowed);
    }

    public async Task RemoveUserMenuPermissionAsync(int userId, int menuId)
        => await db.Deleteable<UserMenuPermission>().Where(x => x.UserId == userId).Where(x => x.MenuId == menuId).ExecuteCommandAsync();

    public async Task RemoveUserApiPermissionAsync(int userId, int apiEndpointId)
        => await db.Deleteable<UserApiPermission>().Where(x => x.UserId == userId).Where(x => x.ApiEndpointId == apiEndpointId).ExecuteCommandAsync();
}