using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public ChangePasswordCommandHandler(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        return await _userService.ChangePasswordAsync(userId, request.OldPassword, request.NewPassword);
    }
}