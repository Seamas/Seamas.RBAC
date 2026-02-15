using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.EnableUser;

public class EnableUserCommandHandler : IRequestHandler<EnableUserCommand, bool>
{
    private readonly IUserService _userService;

    public EnableUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(EnableUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.EnableUserAsync(request.Id, true);
    }
}