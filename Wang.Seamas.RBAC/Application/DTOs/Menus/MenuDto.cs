namespace Wang.Seamas.RBAC.Application.DTOs.Menus;

public class MenuDto
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Path { get; set; }
    public int? ParentId 
    { 
        get; 
        set => field = value <= 0 ? null : value; 
    }
    public int SortOrder { get; set; }
    public bool IsEnabled { get; set; } = true;
}