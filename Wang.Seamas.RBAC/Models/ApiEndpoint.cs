
namespace Wang.Seamas.RBAC.Models;

public class ApiEndpoint
{
    public int Id { get; set; }
    
    /// <summary>
    /// 接口地址
    /// 唯一，如 "/api/users"
    /// </summary>
    public string Url { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    /// <summary>
    ///  是否启用
    /// </summary>
    public bool IsEnabled { get; set; } = true;
}