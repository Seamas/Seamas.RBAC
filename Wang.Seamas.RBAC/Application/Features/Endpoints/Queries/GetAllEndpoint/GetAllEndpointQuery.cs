using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetAllEndpoint;

public class GetAllEndpointQuery: IRequest<List<ApiEndpoint>>
{
    
}