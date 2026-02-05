namespace Wang.Seamas.RBAC.Requests.Auth;

public record RegisterUserRequest(string Username, string Password, string? Nickname, string? Email);