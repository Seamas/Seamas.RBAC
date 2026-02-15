using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.CheckUsername;

public class CheckUsernameCommandHandler : IRequestHandler<CheckUsernameCommand, bool>
{
    
    private readonly IUserService _userService;

    public CheckUsernameCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(CheckUsernameCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CheckUsernameAsync(request.Username);
    }
}