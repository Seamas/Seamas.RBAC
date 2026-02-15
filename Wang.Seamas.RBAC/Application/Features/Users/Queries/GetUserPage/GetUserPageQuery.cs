
using MediatR;
using Wang.Seamas.Queryable.Attributes;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Users.Queries.GetUserPage;

public class GetUserPageQuery : SearchPage, IRequest<ResultPage<UserDto>>
{
    
    [Like(nameof(User.Username))]
    public string? Username { get; set; } 
    [Like(nameof(User.Nickname))]
    public string? Nickname { get; set; }
    [Like(nameof(User.Email))]
    public string? Email { get; set; }
}