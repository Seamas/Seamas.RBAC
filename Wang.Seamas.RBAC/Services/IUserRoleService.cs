using Wang.Seamas.RBAC.Models;

namespace Wang.Seamas.RBAC.Services;

public interface IUserRoleService
{
    // 将用户与一个或多个角色关联
    Task AssignRolesToUserAsync(int userId, List<int> roleIds);

    // 移除用户的所有角色（或指定角色）
    Task RemoveRolesFromUserAsync(int userId, List<int>? roleIds = null);

    // 获取用户当前关联的角色 ID 列表
    Task<List<int>> GetRoleIdsByUserIdAsync(int userId);

    // 获取用户当前关联的角色（含名称、状态）
    Task<List<Role>> GetRolesByUserIdAsync(int userId);
}