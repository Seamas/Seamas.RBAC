namespace Wang.Seamas.RBAC.Requests.ApiEndpoint;

public record SearchApiRequest(int? PageIndex, int? PageSize, string? Url, string? ApiGroup, string? Description, string? IsEnabled) : PagedRequest(PageIndex, PageSize);