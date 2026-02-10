namespace Wang.Seamas.RBAC.Requests.Role;

public record CreateRoleRequest(string Code, string Name, bool? IsEnabled);
