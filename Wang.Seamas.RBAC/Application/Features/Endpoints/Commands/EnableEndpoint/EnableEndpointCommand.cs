using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.EnableEndpoint;

public class EnableEndpointCommand : IRequest<bool>
{
    public int Id { get; set; }
}