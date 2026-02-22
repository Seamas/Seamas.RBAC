

using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Domain.Interfaces;

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
    Task<(List<Role> Roles, int TotalCount)> QueryRolesAsync(SearchPage dto);
    
    Task<bool> DeleteRoleAsync(int roleId);

    Task<bool> CheckCodeAsync(int? id, string code);
    

    Task<List<UserDto>> GetUsersByRoleAsync(int roleId);

    Task<bool> AssignUsersAsync(int roleId, List<int> userIds);
}