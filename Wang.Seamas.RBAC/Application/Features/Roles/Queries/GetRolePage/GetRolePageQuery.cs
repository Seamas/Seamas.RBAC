using MediatR;
using Wang.Seamas.Queryable.Attributes;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Roles.Queries.GetRolePage;

public class GetRolePageQuery : SearchPage, IRequest<ResultPage<Role>>
{
    [Like(nameof(Role.Code))]
    public string? Code { get; set; }
    
    [Like(nameof(Role.Name))]
    public string? Name { get; set; }
}