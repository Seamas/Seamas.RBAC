using SqlSugar;
using Wang.Seamas.Queryable.Helpers;
using Wang.Seamas.RBAC.Application.Features.Users.DTOs;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.Exceptions;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Services;

public class UserService(ISqlSugarClient db, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<int> CreateUserAsync(User user, string password)
    {
        if (await db.Queryable<User>().Where(u => u.Username == user.Username).AnyAsync())
            throw new InvalidOperationException($"用户名{user.Username} 已存在.");

        var id = await db.Insertable(new User
        {
            Username = user.Username,
            Email = user.Email,
            Nickname = user.Nickname,
            PasswordHash = passwordHasher.HashPassword(password),
            IsEnabled = true
        }).ExecuteReturnIdentityAsync();
        return id;
    }

    public async Task<bool> UpdateUserProfileAsync(int userId, string? nickname = null, string? email = null)
    {
        var update = db.Updateable<User>().Where(u => u.Id == userId);
        
        update = update.SetColumns(u => u.Nickname, nickname);
        update = update.SetColumns(u => u.Email, email );
        
        return await update.ExecuteCommandAsync() > 0;
    }

    public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
    {
        var user = await db.Queryable<User>().Where(u => u.Id == userId).FirstAsync();

        if (user == null || !passwordHasher.VerifyPassword(oldPassword, user.PasswordHash))
            throw new BizException("原密码错误");
        
        user.PasswordHash = passwordHasher.HashPassword(newPassword);
        return await db.Updateable(user)
            .UpdateColumns(u => u.PasswordHash)
            .ExecuteCommandAsync() > 0;
    }

    public async Task<bool> ResetPasswordAsync(int userId, string newPassword)
    {
        var hash = passwordHasher.HashPassword(newPassword);
        return await db.Updateable<User>()
            .SetColumns(u => new User { PasswordHash = hash })
            .Where(u => u.Id == userId)
            .ExecuteCommandAsync() > 0;
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await db.Queryable<User>().Where(u => u.Username == username && u.IsEnabled).FirstAsync();
        return user != null && passwordHasher.VerifyPassword(password, user.PasswordHash) ? user : null;
    }
    

    public async Task<bool> EnableUserAsync(int userId, bool enabled)
        => await db.Updateable<User>().SetColumns(u => u.IsEnabled, enabled).Where(u => u.Id == userId).ExecuteCommandAsync() > 0;

    public async Task<UserDto> GetUserByIdAsync(int userId)
        => await db.Queryable<User>()
            .Select(u => new UserDto { Id = u.Id, Username = u.Username, Nickname = u.Nickname, Email = u.Email, IsEnabled = u.IsEnabled })
            .Where(u => u.Id == userId)
            .FirstAsync();

    public async Task<UserDto?> GetUserByUsernameAsync(string username)
        => await db.Queryable<User>()
            .Select(u => new UserDto { Id = u.Id, Username = u.Username, Nickname = u.Nickname, Email = u.Email, IsEnabled = u.IsEnabled })
            .Where(u => u.Username == username)
            .FirstAsync();

    public async Task<(List<UserDto> Users, int TotalCount)> QueryUsersAsync(SearchPage pageDto)
    {
        var query = db.Queryable<User>();
        var expression = QueryHelper.Visit<User>(pageDto);

        query = query.Where(expression);

        var totalCount = await query.CountAsync();
        var users = await query
            .Select(u => new UserDto { Id = u.Id, Username = u.Username, Nickname = u.Nickname, Email = u.Email, IsEnabled = u.IsEnabled })
            .OrderBy(u => u.Id)
            .ToPageListAsync(pageDto.PageIndex, pageDto.PageSize);

        return (users, totalCount);
    }

    public async Task<bool> CheckUsernameAsync(string username)
    {
        var query = db.Queryable<User>();
        query.Where(x => x.Username == username);
        return !await query.AnyAsync();
    }
}