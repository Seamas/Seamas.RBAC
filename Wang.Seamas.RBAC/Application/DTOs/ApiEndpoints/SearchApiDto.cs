using Wang.Seamas.Queryable.Attributes;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.DTOs.ApiEndpoints;

public class SearchApiDto : SearchPage
{
    [Like(nameof(ApiEndpoint.Url))]
    public string? Url { get; set; }
    
    [Like(nameof(ApiEndpoint.ApiGroup))]
    public string? ApiGroup { get; set; }
    
    [Like(nameof(ApiEndpoint.Description))]
    public string? Description { get; set; }
    
    [Equal(nameof(ApiEndpoint.IsEnabled))]
    public bool? IsEnabled { get; set; }
}