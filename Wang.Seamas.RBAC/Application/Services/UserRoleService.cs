using SqlSugar;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Services;

public class UserRoleService(ISqlSugarClient db) : IUserRoleService
{
    public async Task AssignRolesToUserAsync(int userId, List<int> roleIds)
    {
        var userRoles = roleIds.Select(item => new UserRole() {UserId = userId, RoleId = item}).ToList();
        await db.Deleteable<UserRole>().Where(x => x.UserId == userId).ExecuteCommandAsync();
        await db.Insertable<UserRole>(userRoles).ExecuteCommandAsync();
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