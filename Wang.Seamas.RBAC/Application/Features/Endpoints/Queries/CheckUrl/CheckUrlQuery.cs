using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.CheckUrl;

public class CheckUrlQuery : IRequest<bool>
{
    public int? Id { get; set; }
    public required string Url { get; set; }
}