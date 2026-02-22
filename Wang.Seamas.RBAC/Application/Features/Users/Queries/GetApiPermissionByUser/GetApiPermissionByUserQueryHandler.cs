using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetApiPermissionByUser;

public class GetApiPermissionsByUserQueryHandler: IRequestHandler<GetApiPermissionByUserQuery, List<ApiEndpoint>>
{
    private readonly IApiEndpointService _apiEndpointService;

    public GetApiPermissionsByUserQueryHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }

    public async Task<List<ApiEndpoint>> Handle(GetApiPermissionByUserQuery request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.GetApiPermissionsByUserIdAsync(request.UserId);
    }
}