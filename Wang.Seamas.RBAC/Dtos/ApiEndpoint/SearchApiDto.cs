using Wang.Seamas.Queryable.Attributes;

namespace Wang.Seamas.RBAC.Dtos.ApiEndpoint;

public class SearchApiDto : SearchPageDto
{
    [Like(nameof(Models.ApiEndpoint.Url))]
    public string? Url { get; set; }
    
    [Like(nameof(Models.ApiEndpoint.ApiGroup))]
    public string? ApiGroup { get; set; }
    
    [Like(nameof(Models.ApiEndpoint.Description))]
    public string? Description { get; set; }
    
    [Equal(nameof(Models.ApiEndpoint.IsEnabled))]
    public bool? IsEnabled { get; set; }
}