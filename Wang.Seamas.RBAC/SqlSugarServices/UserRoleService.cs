using SqlSugar;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;

namespace Wang.Seamas.RBAC.SqlSugarServices;

public class UserRoleService(ISqlSugarClient db) : IUserRoleService
{
    public async Task AssignRolesToUserAsync(int userId, List<int> roleIds)
    {
        if (roleIds == null || !roleIds.Any()) return;
        
        await db.Ado.UseTranAsync(async () =>
        {
            await db.Deleteable<UserRole>().Where(x => x.UserId == userId).ExecuteCommandAsync();
            var data = roleIds.Select(r => new { user_id = userId, role_id = r }).ToList();
            await db.Insertable(data).ExecuteCommandAsync();
        });
    }

    public async Task RemoveRolesFromUserAsync(int userId, List<int>? roleIds = null)
    {
        if (roleIds == null || !roleIds.Any())
            await db.Deleteable<UserRole>().Where(x => x.UserId == userId).ExecuteCommandAsync();
        else
            await db.Deleteable<UserRole>()
                .Where(x => x.UserId == userId)
                .Where(x => roleIds.Contains(x.RoleId))
                .ExecuteCommandAsync();
    }

    public async Task<List<int>> GetRoleIdsByUserIdAsync(int userId)
        => await db.Queryable<UserRole>().Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();

    public async Task<List<Role>> GetRolesByUserIdAsync(int userId)
        => await db.Queryable<Role>()
            .InnerJoin<UserRole>((r, ur) => r.Id == ur.RoleId)
            .Where((r, ur) => ur.UserId == userId)
            .ToListAsync();
}