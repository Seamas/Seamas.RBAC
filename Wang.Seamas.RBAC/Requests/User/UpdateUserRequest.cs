namespace Wang.Seamas.RBAC.Requests.User;

public record UpdateUserRequest(int Id, string? Nickname, string? Email);