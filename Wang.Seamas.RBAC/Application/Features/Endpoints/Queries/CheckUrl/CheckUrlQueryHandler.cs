using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.CheckUrl;

public class CheckUrlQueryHandler: IRequestHandler<CheckUrlQuery, bool>
{
    private readonly IApiEndpointService _apiEndpointService;

    public CheckUrlQueryHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }

    public async Task<bool> Handle(CheckUrlQuery request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.CheckApiUrlAsync(request.Id, request.Url);
    }
}