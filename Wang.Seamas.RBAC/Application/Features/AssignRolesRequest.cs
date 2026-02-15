namespace Wang.Seamas.RBAC.Requests;

public record AssignRolesRequest(int UserId, List<int> RoleIds);