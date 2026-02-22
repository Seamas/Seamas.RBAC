using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetAllEndpoint;

public class GetAllQueryHandler : IRequestHandler<GetAllEndpointQuery, List<ApiEndpoint>>
{
    private readonly IApiEndpointService _endpointService;

    public GetAllQueryHandler(IApiEndpointService endpointService)
    {
        _endpointService = endpointService;
    }

    public async Task<List<ApiEndpoint>> Handle(GetAllEndpointQuery request, CancellationToken cancellationToken)
    {
        return await _endpointService.GetAllApiEndpointsAsync();
    }
}