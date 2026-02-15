using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.EnableUser;

public class EnableUserCommand : IRequest<bool>
{
    public int Id { get; set; }
}