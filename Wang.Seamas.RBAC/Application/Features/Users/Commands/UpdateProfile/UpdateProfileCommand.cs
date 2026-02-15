using MediatR;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<ProfileResponse>
{
    public required string Nickname { get; set; }
    public required string Email { get; set; }
}