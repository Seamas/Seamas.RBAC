namespace Wang.Seamas.RBAC.Application.Features.Users.DTOs;

public class ProfileResponse
{
    public required string Username { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }
}