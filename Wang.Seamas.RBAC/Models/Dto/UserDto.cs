namespace Wang.Seamas.RBAC.Models.Dto;

/// <summary>
/// 用户数据传输对象（DTO），不暴露密码哈希
/// </summary>
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Nickname { get; set; } = string.Empty;
    public string? Email { get; set; }
    public bool IsEnabled { get; set; }
}