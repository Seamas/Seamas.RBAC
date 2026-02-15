using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.Logout;

public class LogoutUserCommandHandler: IRequestHandler<LogoutUserCommand, bool>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public LogoutUserCommandHandler(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        // TODO:
        return await Task.FromResult(true);
    }
}