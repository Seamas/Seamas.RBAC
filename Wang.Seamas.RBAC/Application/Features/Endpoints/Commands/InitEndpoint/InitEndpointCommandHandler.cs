using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.InitEndpoint;

public class InitEndpointCommandHandler : IRequestHandler<InitEndpointCommand, bool>
{
    private readonly IApiEndpointService _apiEndpointService;

    public InitEndpointCommandHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }


    public async Task<bool> Handle(InitEndpointCommand request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.InitApiEndpointsAsync(request.Endpoints);
    }
}