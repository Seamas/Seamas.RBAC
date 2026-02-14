using Wang.Seamas.RBAC.Dtos.ApiEndpoint;
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
    
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<(List<ApiEndpoint> items, int totolCount)> SearchApiAsync(SearchApiDto dto);
    
    /// <summary>
    /// 根据ID获取API
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ApiEndpoint> GetApiEndpointByIdAsync(int id);


    /// <summary>
    /// 检查URL
    /// </summary>
    /// <param name="id"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<bool> CheckApiUrlAsync(int? id, string url);
    
    
    Task<bool> CreateApiEndpointAsync(ApiEndpoint endpoint);
    
    Task<bool> UpdateApiEndpointAsync(ApiEndpoint endpoint);

    Task<bool> EnableApiEndpointAsync(int id, bool enabled);


    Task<bool> DeleteApiEndpointAsync(int id);

    Task<bool> InitApiEndpointsAsync(IEnumerable<string> apiEndpoints);
}