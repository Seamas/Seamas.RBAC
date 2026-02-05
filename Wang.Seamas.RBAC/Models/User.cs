using SqlSugar;

namespace Wang.Seamas.RBAC.Models;

public class User
{
    /// <summary>
    /// ID主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "ID")]
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