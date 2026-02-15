using SqlSugar;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Services;

public class PermissionChecker(ISqlSugarClient db) : IPermissionChecker
{
    public async Task<bool> IsUserAllowedToAccessApiAsync(int userId, string url)
    {
        // 1. 用户启用？
        if (!await db.Queryable<User>().Where(u => u.Id == userId && u.IsEnabled).AnyAsync()) return false;

        // 2. API 存在且启用？
        var endpoint = await db.Queryable<ApiEndpoint>().Where(a => a.Url == url && a.IsEnabled).FirstAsync();
        if (endpoint == null) return false;

        // 3. 用户特例？
        var userPerm = await db.Queryable<UserApiPermission>()
            .Where(x => x.UserId == userId)
            .Where(x => x.ApiEndpointId == endpoint.Id)
            .FirstAsync();
            
        if (userPerm != null) return userPerm.IsAllowed;

        // 4. 角色权限？
        return await db.Queryable<UserRole>()
            .InnerJoin<Role>((ur, r) => ur.RoleId == r.Id)
            .InnerJoin<RoleApiPermission>((ur, r, rap) => r.Id == rap.RoleId)
            .Where((ur,r, rap) => ur.UserId == userId)
            .Where((ur,r, rap) => rap.ApiEndpointId == endpoint.Id)
            .Where((ur,r, rap) => r.IsEnabled)
            .AnyAsync();
    }
    
}