using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetEndpoint;

public class GetEndpointQueryHandler : IRequestHandler<GetEndpointQuery, ApiEndpoint>
{
    private readonly IApiEndpointService _apiEndpointService;

    public GetEndpointQueryHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }


    public async Task<ApiEndpoint> Handle(GetEndpointQuery request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.GetApiEndpointByIdAsync(request.Id);
    }
}