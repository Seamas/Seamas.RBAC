namespace Wang.Seamas.RBAC.Requests;

public record CreateRoleRequest(string Name, bool? IsEnabled);
