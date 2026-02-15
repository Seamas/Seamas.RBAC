namespace Wang.Seamas.RBAC.Domain.Entities;

public class User
{
    /// <summary>
    /// ID主键
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = null!;
    
    public string? Nickname { get; set; }

    /// <summary>
    /// 密码HASH
    /// </summary>
    public string PasswordHash { get; set; } = null!;
    
    /// <summary>
    /// 邮箱地址
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; } = true; 
}