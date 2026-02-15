using AutoMapper;
using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    
    // TODO: 应该从配置文件读取出来
    private const string Password = "1@3$5^7*";

    public CreateUserCommandHandler(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        return await _userService.CreateUserAsync(user, Password) > 0;
    }
}