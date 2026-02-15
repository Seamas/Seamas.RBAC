using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<bool>
{
    public int Id { get; init; }
}