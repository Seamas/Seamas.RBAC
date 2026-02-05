using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;

namespace Wang.Seamas.RBAC.SqlSugarServices;

public class RoleService(ISqlSugarClient db) : IRoleService
{
    public async Task<int> CreateRoleAsync(string roleName, bool isEnabled = true)
    {
        if (await db.Queryable<Role>().Where(r => r.Name == roleName).AnyAsync())
            throw new InvalidOperationException("Role already exists.");
        return (int)await db.Insertable(new Role { Name = roleName, IsEnabled = isEnabled }).ExecuteReturnIdentityAsync();
    }

    public async Task<bool> UpdateRoleAsync(int roleId, string? name = null, bool? isEnabled = null)
    {
        var update = db.Updateable<Role>().Where(r => r.Id == roleId);
        if (name != null) update = update.SetColumns(r => new Role { Name = name });
        if (isEnabled != null) update = update.SetColumns(r => new Role { IsEnabled = isEnabled.Value });
        return await update.ExecuteCommandAsync() > 0;
    }

    public async Task<bool> SetRoleEnabledAsync(int roleId, bool isEnabled) 
        => await db.Updateable<Role>()
            .SetColumns(r => new Role { IsEnabled = isEnabled })
            .Where(r => r.Id == roleId)
            .ExecuteCommandAsync() > 0;


    public async Task<Role?> GetRoleByIdAsync(int roleId)
        => await db.Queryable<Role>().Where(r => r.Id == roleId).FirstAsync();

    public async Task<List<Role>> GetActiveRolesAsync()
        => await db.Queryable<Role>().Where(r => r.IsEnabled).ToListAsync();

    public async Task<(List<Role> Roles, int TotalCount)> GetRolesAsync(int page, int pageSize, string? keyword = null)
    {
        var query = db.Queryable<Role>();
        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(r => r.Name.Contains(keyword));
        var total = await query.CountAsync();
        var roles = await query.OrderBy(r => r.Id).ToPageListAsync(page, pageSize);
        return (roles, total);
    }
}