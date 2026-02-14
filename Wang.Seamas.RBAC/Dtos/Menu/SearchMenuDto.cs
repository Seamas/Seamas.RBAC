using Wang.Seamas.Queryable.Attributes;

namespace Wang.Seamas.RBAC.Dtos.Menu;

public class SearchMenuDto : SearchPageDto
{
    [Like(nameof(Models.Menu.Name))]
    public string? Name { get; set; }
    
    [Equal(nameof(Models.Menu.Code))]
    public string? Code { get; set; }
    
    [Equal(nameof(Models.Menu.IsEnabled))]
    public bool? IsEnabled { get; set; }
}