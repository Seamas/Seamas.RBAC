namespace Wang.Seamas.RBAC.Requests.Menu;

public record SearchMenuRequest(int? PageIndex, int? PageSize, string? Name, string? Code, bool? IsEnabled) : PagedRequest(PageIndex, PageSize);