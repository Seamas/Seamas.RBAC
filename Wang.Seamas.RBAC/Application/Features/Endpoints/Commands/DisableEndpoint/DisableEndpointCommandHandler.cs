using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.DisableEndpoint;

public class DisableEndpointCommandHandler : IRequestHandler<DisableEndpointCommand, bool>
{
    private readonly IApiEndpointService _apiEndpointService;

    public DisableEndpointCommandHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }

    public async Task<bool> Handle(DisableEndpointCommand request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.EnableApiEndpointAsync(request.Id, false);
    }
}