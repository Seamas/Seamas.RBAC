using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetApiByUser;

public class GetApiByUserQueryHandler: IRequestHandler<GetApiByUserQuery, List<ApiEndpoint>>
{
    private readonly IApiEndpointService _apiEndpointService;

    public GetApiByUserQueryHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }

    public async Task<List<ApiEndpoint>> Handle(GetApiByUserQuery request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.GetApisByUserIdAsync(request.UserId);
    }
}