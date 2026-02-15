using AutoMapper;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.CreateUser;
using Wang.Seamas.RBAC.Application.Features.Users.Commands.Register;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Users;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
        CreateMap<CreateUserCommand, User>();
    }
}