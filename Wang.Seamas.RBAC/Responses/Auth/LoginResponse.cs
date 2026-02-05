namespace Wang.Seamas.RBAC.Responses;

public record LoginResponse(int UserId, string Username, string? Nickname, string Token);