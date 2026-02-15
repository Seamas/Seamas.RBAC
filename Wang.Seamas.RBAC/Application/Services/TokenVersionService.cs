using Microsoft.Extensions.Caching.Distributed;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Services;

/// <summary>
/// Token 版本管理服务,
/// 需要在下发 token 中增加版本号, 并且收到请求 token 时进行验证
/// 在生成 token 时, 需要将 版本号更新到 分布式缓存中
/// </summary>
/// <param name="cache"></param>
public class TokenVersionService(IDistributedCache cache): ITokenVersionService
{
    
    private const string VersionPrefix = "token_version_";

    public async Task<int> GetCurrentVersionAsync(int userId)
    {
        // 先从缓存获取
        var cacheKey = $"{VersionPrefix}{userId}";
        var cachedVersion = await cache.GetStringAsync(cacheKey);
        
        // 如果缓存没有，则默认为0
        return int.TryParse(cachedVersion, out var version) ? version : 0;
    }

    public async Task<int> IncrementVersionAsync(int userId)
    {
        var currentVersionAsync = await GetCurrentVersionAsync(userId);
        currentVersionAsync++;
        var cacheKey = $"{VersionPrefix}{userId}";
        await cache.SetStringAsync(cacheKey, currentVersionAsync.ToString());
        return currentVersionAsync;
    }

    public async Task<bool> ValidateTokenVersionAsync(int userId, int tokenVersion)
    {
        var currentVersionAsync = await GetCurrentVersionAsync(userId);
        return tokenVersion >= currentVersionAsync;
    }
}