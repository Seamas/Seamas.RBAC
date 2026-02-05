namespace Wang.Seamas.RBAC.Requests;

public record UpdateRoleRequest(int Id, string? Name, bool? IsEnabled);
