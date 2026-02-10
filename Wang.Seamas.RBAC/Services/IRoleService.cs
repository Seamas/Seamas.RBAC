using Wang.Seamas.RBAC.Models;
using Wang.Seamas.Web.Common.Dtos;

namespace Wang.Seamas.RBAC.Services;

public interface IRoleService
{
    // 创建角色
    Task<int> CreateRoleAsync(string code, string roleName, bool isEnabled = true);

    // 更新角色
    Task<bool> UpdateRoleAsync(int roleId, string code,  string name);

    // 禁用/启用角色
    Task<bool> SetRoleEnabledAsync(int roleId, bool isEnabled);

    // 获取角色详情
    Task<Role?> GetRoleByIdAsync(int roleId);

    // 获取所有启用的角色（用于分配）
    Task<List<Role>> GetActiveRolesAsync();

    // 分页查询角色
    Task<(List<Role> Roles, int TotalCount)> GetRolesAsync(int page, int pageSize, string? code, string? name);
    
    Task<bool> DeleteRoleAsync(int roleId);

    Task<bool> CheckCodeAsync(int? id, string code);
}