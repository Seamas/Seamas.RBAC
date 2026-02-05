using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Wang.Seamas.Web.Services;

namespace Wang.Seamas.RBAC.Services;

public class TokenBlocklistService(IDistributedCache cache, ILogger<TokenBlocklistService> logger) : ITokenBlocklistService
{
    private const string Prefix = "blocklist_token_";
    
    public async Task BlocklistTokenAsync(string token, TimeSpan? expiry = null)
    {
        try
        {
            // 计算 Token 的哈希作为 key，避免存储原始 Token
            var tokenHash = ComputeTokenHash(token);
            var cacheKey = $"{Prefix}{tokenHash}";

            // 默认黑名单时间为 Token 的剩余有效期
            var absoluteExpiry = DateTimeOffset.UtcNow.Add(expiry ?? TimeSpan.FromHours(2));

            await cache.SetStringAsync(
                cacheKey,
                "blacklisted",
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = absoluteExpiry
                });

            logger.LogInformation($"Token 已加入黑名单，过期时间: {absoluteExpiry}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "将 Token 加入黑名单失败");
            throw;
        }
    }

    public async Task<bool> IsTokenBlocklistedAsync(string token)
    {
        try
        {
            var tokenHash = ComputeTokenHash(token);
            var cacheKey = $"{Prefix}{tokenHash}";

            var value = await cache.GetStringAsync(cacheKey);
            return value != null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "检查 Token 黑名单失败");
            return false; // 发生错误时默认不阻止访问
        }
    }

    public async Task RemoveExpiredTokensAsync()
    {
        // Redis 会自动清理过期 key
        // 如果是其他存储，可以在这里实现清理逻辑
        await Task.CompletedTask;
    }
    
    private string ComputeTokenHash(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}