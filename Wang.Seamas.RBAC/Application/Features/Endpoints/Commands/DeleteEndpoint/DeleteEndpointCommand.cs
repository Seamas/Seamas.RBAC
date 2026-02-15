using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.DeleteEndpoint;

public class DeleteEndpointCommand : IRequest<bool>
{
    public int Id { get; set; }
}