using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.UpdateEndpoint;

public class UpdateEndpointCommand : IRequest<bool>
{
    public int Id { get; set; }
    public required string Url { get; set; } 
    public string? ApiGroup { get; set; } 
    public string? Description { get; set; }
}