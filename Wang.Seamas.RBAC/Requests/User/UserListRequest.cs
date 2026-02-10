namespace Wang.Seamas.RBAC.Requests.User;

public record UserListRequest(int? PageIndex, int? PageSize, string? Username, string? Nickname, string? Email);