using MediatR;
using Wang.Seamas.Queryable.Attributes;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetEndpointPage;

public class GetEndpointPageQuery : SearchPage, IRequest<ResultPage<ApiEndpoint>>
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