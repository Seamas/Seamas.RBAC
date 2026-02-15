using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.DeleteEndpoint;

public class DeleteEndpointCommandHandler : IRequestHandler<DeleteEndpointCommand, bool>
{
    private readonly IApiEndpointService _apiEndpointService;

    public DeleteEndpointCommandHandler(IApiEndpointService apiEndpointService)
    {
        _apiEndpointService = apiEndpointService;
    }

    public async Task<bool> Handle(DeleteEndpointCommand request, CancellationToken cancellationToken)
    {
        return await _apiEndpointService.DeleteApiEndpointAsync(request.Id);
    }
}