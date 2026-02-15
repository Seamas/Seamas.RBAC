using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetEndpointPage;

public class GetEndpointPageQueryHandler : IRequestHandler<GetEndpointPageQuery, ResultPage<ApiEndpoint>>
{
    private readonly IApiEndpointService _apiEndpointService;

    public GetEndpointPageQueryHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }

    public async Task<ResultPage<ApiEndpoint>> Handle(GetEndpointPageQuery request, CancellationToken cancellationToken)
    {
        var (list, total) = await _apiEndpointService.QueryEndpointAsync(request);
        return new ResultPage<ApiEndpoint>(list, total, request.PageIndex, request.PageSize);
    }
}