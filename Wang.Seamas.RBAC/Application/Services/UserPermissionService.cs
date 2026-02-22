using SqlSugar;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Services;

public class UserPermissionService(ISqlSugarClient db) : IUserPermissionService
{

    public async Task SetUserMenuPermissionsAsync(int userId, List<int> menuIds)
    {
        await db.Deleteable<UserMenuPermission>().Where(x => x.UserId == userId).ExecuteCommandAsync();
        var data = menuIds.Select(item => new UserMenuPermission() { UserId = userId, MenuId = item }).ToList();
        if (data.Count > 0)
        {
            await db.Insertable<UserMenuPermission>(data).ExecuteCommandAsync();
        }
    }
    

    public async Task SetUserApiPermissionsAsync(int userId, List<int> apiEndpointIds)
    {
        await db.Deleteable<UserApiPermission>().Where(x => x.UserId == userId).ExecuteCommandAsync();
        var data = apiEndpointIds.Select(item => new UserApiPermission(){UserId = userId, ApiEndpointId = item}).ToList();
        if (data.Count > 0)
        {
            await db.Insertable<UserApiPermission>(data).ExecuteCommandAsync();
        }
    }
    
    public async Task RemoveDeprecatedMenuPermissionAsync()
    {
        await db.Deleteable<UserMenuPermission>()
            .Where(ump => SqlFunc.Subqueryable<UserRole>()
                .InnerJoin<Role>((ur, r) => ur.RoleId == r.Id)
                .InnerJoin<RoleMenuPermission>((ur, r, rmp) => r.Id == rmp.RoleId)
                .Where((ur, r, rmp) => ump.MenuId == rmp.MenuId && ump.UserId == ur.UserId)
                .NotAny())
            .ExecuteCommandAsync();
    }

    public async Task RemoveDeprecatedApiPermissionAsync()
    {
        await db.Deleteable<UserApiPermission>()
            .Where(uap => SqlFunc.Subqueryable<UserRole>()
                .InnerJoin<Role>((ur, r) => ur.RoleId == r.Id)
                .InnerJoin<RoleApiPermission>((ur, r, rap) => r.Id == rap.RoleId)
                .Where((ur, r, rap) => uap.ApiEndpointId == rap.ApiEndpointId && uap.UserId == ur.UserId)
                .NotAny())
            .ExecuteCommandAsync();
    }

    public async Task RemoveDeprecatedUserMenuPermissionAsync(int userId)
    {
        await db.Deleteable<UserMenuPermission>()
            .Where(ump => ump.UserId == userId)
            .Where(ump =>  SqlFunc.Subqueryable<UserRole>()
                .InnerJoin<Role>((ur, r) => ur.RoleId == r.Id)
                .InnerJoin<RoleMenuPermission>((ur, r, rmp) => r.Id == rmp.RoleId)
                .Where((ur, r, rmp) => ump.MenuId == rmp.MenuId && ump.UserId == ur.UserId)
                .NotAny())
            .ExecuteCommandAsync();
    }

    public async Task RemoveDeprecatedUserApiPermissionAsync(int userId)
    {
        await db.Deleteable<UserApiPermission>()
            .Where(uap => uap.UserId == userId)
            .Where(uap => SqlFunc.Subqueryable<UserRole>()
                .InnerJoin<Role>((ur, r) => ur.RoleId == r.Id)
                .InnerJoin<RoleApiPermission>((ur, r, rap) => r.Id == rap.RoleId)
                .Where((ur, r, rap) => uap.ApiEndpointId == rap.ApiEndpointId && uap.UserId == ur.UserId)
                .NotAny())
            .ExecuteCommandAsync();
    }
}