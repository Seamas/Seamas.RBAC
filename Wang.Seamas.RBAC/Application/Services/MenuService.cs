using AutoMapper;
using SqlSugar;
using Wang.Seamas.Queryable.Helpers;
using Wang.Seamas.RBAC.Application.DTOs.Menus;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;


namespace Wang.Seamas.RBAC.Application.Services;

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
        => await db.Queryable<Menu>().ToListAsync();

    public async Task<List<Menu>> GetMenusByRoleIdAsync(int roleId)
    {
        return await db.Queryable<Menu>()
            .InnerJoin<RoleMenuPermission>((m, rm) => m.Id == rm.MenuId)
            .Where((m, rm) => rm.RoleId == roleId)
            .ToListAsync();
    }

    public async Task<List<Menu>> GetMenusByUserIdAsync(int userId)
    {
        var list =  await db.Queryable<Menu>()
            .InnerJoin<RoleMenuPermission>((m, rm) => m.Id == rm.MenuId)
            .InnerJoin<UserRole>((m, rm, ur) => ur.RoleId == rm.RoleId && ur.UserId == userId)
            .Where((m, rm, ur) => ur.UserId == userId)
            .Distinct()
            .ToListAsync();

        var parentIds = list.Where(item => item.ParentId != null)
            .Select(item => item.ParentId!.Value)
            .Distinct()
            .ToList();

        if (parentIds.Count > 0)
        {
            var parents = await db.Queryable<Menu>()
                .Where(m => parentIds.Contains(m.Id))
                .ToListAsync();

            list.AddRange(parents);
            list = list.DistinctBy(item => item.Id).ToList();
        }

        return list;
    }

    public async Task<List<Menu>> GetMenuPermissionsByUserIdAsync(int userId)
    {
        return await db.Queryable<Menu>()
            .InnerJoin<UserMenuPermission>((m, um) => m.Id == um.MenuId && um.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Menu>> GetChildrenMenusAsync(int parentId)
        => await db.Queryable<Menu>().Where(m => m.ParentId == parentId)
            .ToListAsync();

    public async Task<Menu?> GetMenuByIdAsync(int menuId)
        => await db.Queryable<Menu>().Where(m => m.Id == menuId).FirstAsync();

    /// <summary>
    /// 获取用户可见的菜单列表
    /// 1: 菜单必须启用状态, 用户关联的角色必须处于启用状态，否则该角色关联的菜单应该失效
    /// 2: 获取菜单后，需要再次获取父级菜单，父级菜单也应该处于启用状态
    /// 3: 最后筛选parentId != null，但是列表中没有 父级菜单的子菜单
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<Menu>> GetUserVisibleMenusAsync(int userId)
    {
        // 1 获取角色授权的菜单列表
        var list =  await db.Queryable<Menu>()
            .InnerJoin<RoleMenuPermission>((m, rm) => m.Id == rm.MenuId && m.IsEnabled)
            .InnerJoin<UserRole>((m, rm, ur) => ur.RoleId == rm.RoleId && ur.UserId == userId)
            .InnerJoin<Role>((m, rm, ur, r) => r.IsEnabled)
            // 同时需要 not exists 
            .Where((m, rm, ur, r) => SqlFunc.Subqueryable<UserMenuPermission>()
                .Where((x) => x.MenuId == m.Id).NotAny())
            .Distinct()
            .ToListAsync();

        // 2: 
        var parentIds = list.Where(item => item.ParentId != null)
            .Select(item => item.ParentId!.Value)
            .Distinct()
            .ToList();
        if (parentIds.Count > 0)
        {
            var parents = await db.Queryable<Menu>()
                .Where(m => parentIds.Contains(m.Id))
                .ToListAsync();
            list.AddRange(parents);
            list = list.DistinctBy(item => item.Id).ToList();
        }
        
        // 3
        var hashSet = list.Select(item => item.Id).ToHashSet();
        list = list
            .Where(item => item.ParentId == null || hashSet.Contains(item.ParentId.Value) )
            .ToList();
        
        return list;
    }

    public async Task<(List<Menu> Menus, int TotalCount)> QueryMenusAsync(SearchPage dto)
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