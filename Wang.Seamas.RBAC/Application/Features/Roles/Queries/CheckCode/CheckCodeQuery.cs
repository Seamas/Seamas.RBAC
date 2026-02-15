using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.CheckCode;

public class CheckCodeQuery : IRequest<bool>
{
    public int? Id { get; set; }
    public required string Code { get; set; }
}