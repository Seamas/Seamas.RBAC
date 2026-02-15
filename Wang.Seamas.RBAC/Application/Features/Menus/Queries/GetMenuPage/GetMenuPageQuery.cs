using MediatR;
using Wang.Seamas.Queryable.Attributes;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetMenuPage;

public class GetMenuPageQuery : SearchPage, IRequest<ResultPage<Menu>>
{
    [Like(nameof(Menu.Name))]
    public string? Name { get; set; }
    [Like(nameof(Menu.Code))]
    public string? Code { get; set; } 
    [Equal(nameof(Menu.IsEnabled))]
    public bool? IsEnabled { get; set; }
}