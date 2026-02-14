namespace Wang.Seamas.RBAC.Requests.Role;

public record SearchRoleRequest(int? PageIndex, int? PageSize, string? Code, string? Name) : PagedRequest(PageIndex, PageSize);