using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.CreateEndpoint;

public class CreateEndpointCommand: IRequest<bool>
{
    public required string Url { get; set; } 
    public string? ApiGroup { get; set; } 
    public string? Description { get; set; }
}