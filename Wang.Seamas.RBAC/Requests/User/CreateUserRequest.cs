namespace Wang.Seamas.RBAC.Requests.User;

public record CreateUserRequest(string Username, string? Nickname, string? Email);