using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler: IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleService _roleService;
    
    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return await  _roleService.DeleteRoleAsync(request.Id);
    }
}