namespace Wang.Seamas.RBAC.Requests.ApiEndpoint;

public record UpdateApiRequest(int Id, string Url, string? ApiGroup, string? Description, bool IsEnabled);