namespace Wang.Seamas.RBAC.Domain.Interfaces;

public interface IUserPermissionService
{
    
    // 批量设置用户菜单权限
    Task SetUserMenuPermissionsAsync(int userId, List<int> menuIds);

    
    // 批量设置用户 API 权限
    Task SetUserApiPermissionsAsync(int userId, List<int> apiEndpointIds);
    

    /// <summary>
    /// 清除过期的用户-菜单权限
    /// </summary>
    /// <returns></returns>
    Task RemoveDeprecatedMenuPermissionAsync();
    
    /// <summary>
    /// 清除过期的用户-接口权限
    /// </summary>
    /// <returns></returns>
    Task RemoveDeprecatedApiPermissionAsync();
    
    /// <summary>
    /// 移除用户的过期菜单权限
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task RemoveDeprecatedUserMenuPermissionAsync(int userId);
    
    /// <summary>
    /// 移除用户的过期API权限
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task RemoveDeprecatedUserApiPermissionAsync(int userId);
}