
using Wang.Seamas.RBAC.Application.DTOs.Menus;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Domain.Interfaces;

public interface IMenuService
{
    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<int> CreateMenuAsync(MenuDto dto);

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<bool> UpdateMenuAsync(MenuDto dto);
    
    /// <summary>
    /// 获取所有一级菜单
    /// </summary>
    /// <returns></returns>
    Task<List<Menu>> GetFirstLevelMenusAsync();
    
    /// <summary>
    /// 根据父级菜单ID，获取所有的子级菜单
    /// </summary>
    /// <param name="parentId"></param>
    /// <returns></returns>
    Task<List<Menu>> GetChildrenMenusAsync(int parentId);
    
    
    /// <summary>
    /// 根据 ID 获取菜单
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    Task<Menu?> GetMenuByIdAsync(int menuId);


    Task<List<Menu>> GetUserVisibleMenusAsync(int userId);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<(List<Menu> Menus, int TotalCount)> QueryMenusAsync(SearchPage dto);


    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    Task<bool> DeleteMenuAsync(int menuId);
    
    /// <summary>
    /// 启用/禁用菜单
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="enabled"></param>
    /// <returns></returns>
    Task<bool> EnableMenuAsync(int menuId, bool enabled);

    
}