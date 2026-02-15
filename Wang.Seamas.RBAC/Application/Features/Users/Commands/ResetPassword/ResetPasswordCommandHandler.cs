using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly IUserService _userService;
    // TODO: 应该从配置文件读取出来
    private const string Password = "1@3$5^7*";

    public ResetPasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    
    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _userService.ResetPasswordAsync(request.Id, Password);
    }
}