using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.DisableRole;

public class DisableRoleCommandHandler : IRequestHandler<DisableRoleCommand, bool>
{
    private readonly IRoleService _roleService;

    public DisableRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<bool> Handle(DisableRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.SetRoleEnabledAsync(request.Id, false);
    }
}