namespace Wang.Seamas.RBAC.Services;

public interface IRolePermissionService
{
    // 为角色分配菜单权限（覆盖式：先清空再设置）
    Task SetRoleMenuPermissionsAsync(int roleId, List<int> menuIds);

    // 为角色分配 API 权限（覆盖式）
    Task SetRoleApiPermissionsAsync(int roleId, List<int> apiEndpointIds);

    // 获取角色拥有的菜单 ID 列表
    Task<List<int>> GetMenuIdsByRoleIdAsync(int roleId);

    // 获取角色拥有的 API ID 列表
    Task<List<int>> GetApiEndpointIdsByRoleIdAsync(int roleId);
}