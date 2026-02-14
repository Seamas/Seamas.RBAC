namespace Wang.Seamas.RBAC.Requests.Menu;

public record UpdateMenuRequest(int Id, string? Name, string? Code, string? Path, int? ParentId, int? SortOrder, bool? IsEnabled);
