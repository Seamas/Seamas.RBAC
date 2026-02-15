using MediatR;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.Utils;
using Wang.Seamas.Web.DTOs;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Users.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.AuthenticateAsync(request.Username, request.Password);
        Assert.NotNull(user, "用户名或者密码错误");
        var authResult = new AuthResult
        {
            IsAuthenticated = true,
            UserId = user!.Id,
            Username = user.Username,
            Email = user.Email!
        };
        
        var token = _tokenService.GenerateToken(authResult);
        return new LoginUserResponse
        {
            Username = user.Username,
            Nickname =  user.Nickname ?? string.Empty,
            Token = token,
        };
    }
}