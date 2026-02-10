namespace Wang.Seamas.RBAC.Requests.Role;

public record RoleListRequest(int? PageIndex, int? PageSize, string? Code, string? Name);