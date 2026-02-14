namespace Wang.Seamas.RBAC.Requests.ApiEndpoint;

public record CreateApiRequest(string Url, string? ApiGroup, string? Description, bool IsEnabled);