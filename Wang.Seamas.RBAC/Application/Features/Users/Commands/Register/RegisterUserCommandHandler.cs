using AutoMapper;
using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public RegisterUserCommandHandler(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        return await _userService.CreateUserAsync(user, request.Password) > 0;
        
    }
}