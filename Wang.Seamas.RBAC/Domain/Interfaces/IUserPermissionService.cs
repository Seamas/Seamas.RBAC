namespace Wang.Seamas.RBAC.Domain.Interfaces;

public interface IUserPermissionService
{
    // 设置用户对某菜单的显式权限（允许/拒绝）
    Task SetUserMenuPermissionAsync(int userId, int menuId, bool isAllowed);

    // 批量设置用户菜单权限
    Task SetUserMenuPermissionsAsync(int userId, Dictionary<int, bool> permissions);

    // 设置用户对某 API 的显式权限
    Task SetUserApiPermissionAsync(int userId, int apiEndpointId, bool isAllowed);

    // 批量设置用户 API 权限
    Task SetUserApiPermissionsAsync(int userId, Dictionary<int, bool> permissions);

    // 清除用户对某菜单的特例权限（回退到角色）
    Task RemoveUserMenuPermissionAsync(int userId, int menuId);

    // 清除用户对某 API 的特例权限
    Task RemoveUserApiPermissionAsync(int userId, int apiEndpointId);
}