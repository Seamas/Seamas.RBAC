using SqlSugar;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Common.Exceptions;

namespace Wang.Seamas.RBAC.SqlSugarServices;

public class RoleService(ISqlSugarClient db) : IRoleService
{
    public async Task<int> CreateRoleAsync(string code, string roleName, bool isEnabled = true)
    {
        if (await db.Queryable<Role>().Where(r => r.Code == code).AnyAsync())
            throw new BizException($"角色{code}已经存在");
        return await db.Insertable(new Role { Code = code, Name = roleName, IsEnabled = isEnabled }).ExecuteReturnIdentityAsync();
    }

    public async Task<bool> UpdateRoleAsync(int roleId, string code,  string name)
    {
        var update = db.Updateable<Role>().Where(r => r.Id == roleId);

        if (!string.IsNullOrEmpty(name))
        {
            update = update.SetColumns(r => r.Name, name);
        }
        if (!string.IsNullOrEmpty(code))
        {
            update = update.SetColumns(r => r.Code, code);
        }
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

    public async Task<(List<Role> Roles, int TotalCount)> GetRolesAsync(int page, int pageSize, string? code, string? name)
    {
        var query = db.Queryable<Role>();
        if (!string.IsNullOrEmpty(code))
        {
            query = query.Where(r => r.Code.Contains(code));
        }

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(r => r.Name != null && r.Name.Contains(name));
        }
        
        var total = await query.CountAsync();
        var roles = await query.OrderBy(r => r.Id).ToPageListAsync(page, pageSize);
        return (roles, total);
    }

    public async Task<bool> DeleteRoleAsync(int roleId) 
        => await db.Deleteable<Role>()
            .Where(x => x.Id == roleId)
            .ExecuteCommandAsync() > 0;

    public async Task<bool> CheckCodeAsync(int? id, string code)
    {
        var query = db.Queryable<Role>();
        if (id != null && id > 0)
        {
            query = query.Where(r => r.Id != id);
        }
        query.Where(r => r.Code == code);
        return !await query.AnyAsync();
    }
}