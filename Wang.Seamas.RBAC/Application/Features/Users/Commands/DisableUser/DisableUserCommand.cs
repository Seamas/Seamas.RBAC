using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.DisableUser;

public class DisableUserCommand: IRequest<bool>
{
    public int Id { get; set; }
}