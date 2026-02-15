using Wang.Seamas.RBAC.Application.Services;

namespace Wang.Seamas.RBAC.Configure;


public static class SqlSugarRegisterTypes
{
    public static readonly Type[] Types =
    [
        typeof(ApiEndpointService),
        typeof(BcryptPasswordHasher),
        typeof(MenuService),
        typeof(PermissionChecker),
        typeof(RolePermissionService),
        typeof(RoleService),
        typeof(UserPermissionService),
        typeof(UserRoleService),
        typeof(UserService)
    ];
}