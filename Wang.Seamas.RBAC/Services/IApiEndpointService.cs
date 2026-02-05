using Wang.Seamas.RBAC.Models;

namespace Wang.Seamas.RBAC.Services;

public interface IApiEndpointService
{
    // 注册或更新 API 端点（幂等：URL 唯一）
    Task<int> UpsertApiEndpointAsync(string url, string? description = null, bool isEnabled = true);

    // 批量注册 API（如从控制器反射自动注册）
    Task BulkUpsertApiEndpointsAsync(IEnumerable<(string Url, string? Description)> endpoints);

    // 更新 API 启用状态或描述
    Task<bool> UpdateApiEndpointAsync(int id, string? description = null, bool? isEnabled = null);

    // 获取所有 API 列表
    Task<List<ApiEndpoint>> GetAllApiEndpointsAsync();

    // 根据 URL 获取
    Task<ApiEndpoint?> GetApiEndpointByUrlAsync(string url);
}