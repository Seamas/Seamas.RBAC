using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Models.Dto;

namespace Wang.Seamas.RBAC.Services;

public interface IUserService
{
    // 创建新用户（需提供初始密码）
    Task<int> CreateUserAsync(string username, string password, string? nickname, string? email = null, bool isEnabled = true);

    // 更新用户基本信息（不包含密码）
    Task<bool> UpdateUserProfileAsync(int userId, string? nickname, string? email = null);

    // 修改用户密码（需验证旧密码，或由管理员强制重置）
    Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    
    Task<bool> ResetPasswordAsync(int userId, string newPassword); // 管理员重置，无需旧密码

    // 验证用户凭据（用于登录）
    Task<User?> AuthenticateAsync(string username, string password);
    
    // 启用用户
    Task<bool> EnableUserAsync(int userId, bool enabled);

    // 获取用户（不含密码哈希，安全考虑）
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<UserDto?> GetUserByUsernameAsync(string username);

    // 分页查询用户列表（返回脱敏 DTO）
    Task<(List<UserDto> Users, int TotalCount)> GetUsersAsync(
        int page, int pageSize, string? username, string? nickname,  string? email);


    Task<bool> CheckUsernameAsync(string username);
}