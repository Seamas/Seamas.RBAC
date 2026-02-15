using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetRolePage;

public class GetRolePageQueryHandler: IRequestHandler<GetRolePageQuery, ResultPage<Role>>
{
    private readonly IRoleService _roleService;

    public GetRolePageQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<ResultPage<Role>> Handle(GetRolePageQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await _roleService.QueryRolesAsync(request);
        
        return new ResultPage<Role>(items, total, request.PageIndex, request.PageSize);
    }
}