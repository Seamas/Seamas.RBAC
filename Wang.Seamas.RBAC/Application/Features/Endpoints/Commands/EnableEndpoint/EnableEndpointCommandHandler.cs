using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.EnableEndpoint;

public class EnableEndpointCommandHandler : IRequestHandler<EnableEndpointCommand, bool>
{
    private readonly IApiEndpointService _apiEndpointService;

    public EnableEndpointCommandHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }

    public async Task<bool> Handle(EnableEndpointCommand request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.EnableApiEndpointAsync(request.Id, true);
    }
}