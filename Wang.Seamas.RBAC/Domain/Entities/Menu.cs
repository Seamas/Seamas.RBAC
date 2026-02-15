namespace Wang.Seamas.RBAC.Domain.Entities;

public class Menu
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 菜单名称
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// 菜单编码
    /// </summary>
    public string? Code { get; set; }
    
    /// <summary>
    /// 菜单路径
    /// </summary>
    public string? Path { get; set; }
    
    /// <summary>
    /// 父节点
    /// </summary>
    public int? ParentId { get; set; }
    
    /// <summary>
    /// 顺序
    /// </summary>
    public int SortOrder { get; set; } 

    /// <summary>
    ///  是否启用
    /// </summary>
    public bool IsEnabled { get; set; } = true;
}