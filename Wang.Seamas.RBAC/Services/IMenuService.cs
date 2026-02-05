using Wang.Seamas.RBAC.Models;

namespace Wang.Seamas.RBAC.Services;

public interface IMenuService
{
    // 创建菜单
    Task<int> CreateMenuAsync(string name, string? code = null, string? path = null,
        int? parentId = null, int order = 0, bool isEnabled = true);

    // 更新菜单
    Task<bool> UpdateMenuAsync(int menuId, string? name = null, string? code = null,
        string? path = null, int? parentId = null, int? order = null, bool? isEnabled = null);

    // 获取所有菜单（树形结构，仅启用的）
    Task<List<Menu>> GetActiveMenusAsync();

    // 获取所有菜单（含禁用，用于管理后台）
    Task<List<Menu>> GetAllMenusAsync();

    // 根据 ID 获取菜单
    Task<Menu?> GetMenuByIdAsync(int menuId);


    Task<List<Menu>> GetUserVisibleMenusAsync(int userId);

}