using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.InitEndpoint;

public class InitEndpointCommand : IRequest<bool>
{
    public required IEnumerable<string> Endpoints { get; set; }
}