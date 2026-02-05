namespace Wang.Seamas.RBAC.Responses.Auth;

public record ProfileResponse(string Username, string? Nickname, string? Email);