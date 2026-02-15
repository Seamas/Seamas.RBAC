using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetEndpoint;

public class GetEndpointQuery : IRequest<ApiEndpoint>
{
    public int Id { get; set; }
}