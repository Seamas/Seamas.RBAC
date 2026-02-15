
namespace Wang.Seamas.RBAC.Domain.Entities;

public class ApiEndpoint
{
    public int Id { get; set; }
    
    /// <summary>
    /// 接口地址
    /// 唯一，如 "/api/users"
    /// </summary>
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// 接口分组
    /// </summary>
    public string? ApiGroup { get; set; }
    
    public string? Description { get; set; }
    
    /// <summary>
    ///  是否启用
    /// </summary>
    public bool IsEnabled { get; set; } = true;
}