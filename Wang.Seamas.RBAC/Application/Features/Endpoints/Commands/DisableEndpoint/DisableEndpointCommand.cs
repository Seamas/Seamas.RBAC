using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.DisableEndpoint;

public class DisableEndpointCommand : IRequest<bool>
{
    public int Id { get; set; }
}