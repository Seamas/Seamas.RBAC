using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.CheckCode;

public class CheckCodeQueryHandler : IRequestHandler<CheckCodeQuery, bool>
{
    private readonly IRoleService _roleService;

    public CheckCodeQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<bool> Handle(CheckCodeQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.CheckCodeAsync(request.Id, request.Code);
    }
}