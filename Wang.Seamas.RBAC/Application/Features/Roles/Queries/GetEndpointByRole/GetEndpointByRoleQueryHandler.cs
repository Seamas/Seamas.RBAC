using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetEndpointByRole;

public class GetEndpointByRoleQueryHandler : IRequestHandler<GetEndpointByRoleQuery, List<ApiEndpoint>>
{
    private readonly IApiEndpointService _apiEndpointService;

    public GetEndpointByRoleQueryHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }


    public async Task<List<ApiEndpoint>> Handle(GetEndpointByRoleQuery request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.GetApiEndpointByRoleAsync(request.RoleId);
    }
}