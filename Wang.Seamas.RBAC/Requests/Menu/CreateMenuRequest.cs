namespace Wang.Seamas.RBAC.Requests.Menu;

public record CreateMenuRequest(string Name, string? Code, string? Path, int? ParentId, int? SortOrder, bool? IsEnabled);
