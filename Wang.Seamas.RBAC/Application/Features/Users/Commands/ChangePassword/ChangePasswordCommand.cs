using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<bool>
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}