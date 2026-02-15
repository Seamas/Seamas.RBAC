using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.DisableUser;

public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, bool>
{
    private readonly IUserService _userService;

    public DisableUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(DisableUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.EnableUserAsync(request.Id, false);
    }
}