using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.EnableRole;

public class EnableRoleCommandHandler : IRequestHandler<EnableRoleCommand, bool>
{
    
    private readonly IRoleService _roleService;
    
    public EnableRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<bool> Handle(EnableRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.SetRoleEnabledAsync(request.Id, true);
    }
}