using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.CheckUsername;

public class CheckUsernameCommand : IRequest<bool>
{
    public required string Username { get; set; }
}