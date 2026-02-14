using AutoMapper;
using SqlSugar;
using Wang.Seamas.Queryable.Helpers;
using Wang.Seamas.RBAC.Dtos.Menu;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;

namespace Wang.Seamas.RBAC.SqlSugarServices;

public class MenuService(ISqlSugarClient db, IMapper mapper) : IMenuService
{
    public async Task<int> CreateMenuAsync(MenuDto dto)
    {
        var menu = mapper.Map<Menu>(dto);
        return await db.Insertable(menu)
            .ExecuteReturnIdentityAsync();
    }

    public async Task<bool> UpdateMenuAsync(MenuDto dto)
    {
        var menu = mapper.Map<Menu>(dto);
        return await db.Updateable(menu).ExecuteCommandAsync() > 0;
    }

    public async Task<List<Menu>> GetActiveMenusAsync()
        => await db.Queryable<Menu>().Where(m => m.IsEnabled).OrderBy(m => m.SortOrder).ToListAsync();

    public async Task<List<Menu>> GetAllMenusAsync()
        => await db.Queryable<Menu>().OrderBy(m => m.SortOrder).ToListAsync();

    public async Task<List<Menu>> GetChildrenMenusAsync(int parentId)
        => await db.Queryable<Menu>().Where(m => m.ParentId == parentId)
            .ToListAsync();

    public async Task<Menu?> GetMenuByIdAsync(int menuId)
        => await db.Queryable<Menu>().Where(m => m.Id == menuId).FirstAsync();

    public async Task<List<Menu>> GetUserVisibleMenusAsync(int userId)
    {
        if (!await db.Queryable<User>().Where(u => u.Id == userId && u.IsEnabled).AnyAsync())
            return new List<Menu>();

        // 获取角色授予的菜单ID（去重）
        var roleMenuIds = await db.Queryable<UserRole>()
            .InnerJoin<Role>((ur, r) => ur.RoleId == r.Id)
            .InnerJoin<RoleMenuPermission>((ur, r, rmp) => r.Id == rmp.RoleId)
            .InnerJoin<Menu>((ur, r, rmp, m) => rmp.MenuId == m.Id)
            .Where((ur, r, rmp, m) => ur.UserId == userId)
            .Where((ur, r, rmp, m) => r.IsEnabled)
            .Where((ur, r, rmp, m) => m.IsEnabled)
            .Select((ur, r, rmp, m) => m.Id)
            .ToListAsync();

        // 获取用户覆盖
        var overrides = await db.Queryable<UserMenuPermission>()
            .Where(x => x.UserId == userId)
            .Select(x => new { x.MenuId, x.IsAllowed })
            .ToListAsync();


        var overrideDict = overrides.ToDictionary(
            x => x.MenuId,
            x => x.IsAllowed
        );

        var candidateIds = new HashSet<int>(roleMenuIds);
        foreach (var key in overrideDict.Keys)
        {
            candidateIds.Add(key);
        }

        if (!candidateIds.Any())
        {
            return [];
        }

        // 查询菜单详情
        var menus = await db.Queryable<Menu>()
            .Where(m => SqlFunc.Equals(m.Id, candidateIds.ToArray()))
            .Where(m => m.IsEnabled)
            .ToListAsync();

        // 应用覆盖逻辑
        return menus.Where(m =>
        {
            if (overrideDict.TryGetValue(m.Id, out var allowed))
                return allowed;
            return true;
        }).ToList();
    }

    public async Task<(List<Menu> Menus, int TotalCount)> QueryMenusAsync(SearchMenuDto dto)
    {
        var query = db.Queryable<Menu>();
        
        var expression = QueryHelper.Visit<Menu>(dto);
        query = query.Where(expression);
        var totalCount = await query.CountAsync();
        var menus =  await query.ToPageListAsync(dto.PageIndex, dto.PageSize);
        return  (menus, totalCount);
    }

    public async Task<bool> DeleteMenuAsync(int menuId)
        => await db.Deleteable<Menu>()
            .Where(m => m.Id == menuId).ExecuteCommandAsync() > 0;

    public async Task<bool> EnableMenuAsync(int menuId, bool enabled)
    {
        return await  db.Updateable<Menu>()
            .SetColumns(m => m.IsEnabled, enabled)
            .Where(m => m.Id == menuId)
            .ExecuteCommandAsync() > 0;
    }

    public async Task<List<Menu>> GetFirstLevelMenusAsync()
    {
        return await db.Queryable<Menu>()
            .Where(x => x.ParentId == null)
            .ToListAsync();
    }
}