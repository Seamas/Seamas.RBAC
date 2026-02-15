namespace Wang.Seamas.RBAC.Requests;

public record SetRoleMenuPermissionsRequest(int RoleId, List<int> MenuIds);
