using SqlSugar;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;

namespace Wang.Seamas.RBAC.SqlSugarServices;

public class MenuService(ISqlSugarClient db) : IMenuService
{
    public async Task<int> CreateMenuAsync(string name, string? code = null, string? path = null,
        int? parentId = null, int sortOrder = 0, bool isEnabled = true)
    {
        return (int)await db.Insertable(new Menu
        {
            Name = name,
            Code = code,
            Path = path,
            ParentId = parentId,
            SortOrder = sortOrder,
            IsEnabled = isEnabled
        }).ExecuteReturnIdentityAsync();
    }

    public async Task<bool> UpdateMenuAsync(int menuId, string? name = null, string? code = null,
        string? path = null, int? parentId = null, int? sortOrder = null, bool? isEnabled = null)
    {
        var m = new Menu { Id = menuId };
        if (name != null) m.Name = name;
        if (code != null) m.Code = code;
        if (path != null) m.Path = path;
        m.ParentId = parentId;
        if (sortOrder != null) m.SortOrder = sortOrder.Value;
        if (isEnabled != null) m.IsEnabled = isEnabled.Value;

        var update = db.Updateable(m);
        var cols = new List<string>();
        if (name != null) cols.Add(nameof(Menu.Name));
        if (code != null) cols.Add(nameof(Menu.Code));
        if (path != null) cols.Add(nameof(Menu.Path));
        cols.Add(nameof(Menu.ParentId));
        if (sortOrder != null) cols.Add(nameof(Menu.SortOrder));
        if (isEnabled != null) cols.Add(nameof(Menu.IsEnabled));

        return await update.UpdateColumns(cols.ToArray()).ExecuteCommandAsync() > 0;
    }

    public async Task<List<Menu>> GetActiveMenusAsync()
        => await db.Queryable<Menu>().Where(m => m.IsEnabled).OrderBy(m => m.SortOrder).ToListAsync();

    public async Task<List<Menu>> GetAllMenusAsync()
        => await db.Queryable<Menu>().OrderBy(m => m.SortOrder).ToListAsync();

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
}