using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Domain.Interfaces;

public interface IApiEndpointService
{
    
    // 更新 API 启用状态或描述
    Task<bool> UpdateApiEndpointAsync(int id, string? description = null, bool? isEnabled = null);

    // 获取所有 API 列表
    Task<List<ApiEndpoint>> GetAllApiEndpointsAsync();


    Task<List<ApiEndpoint>> GetApiEndpointByRoleAsync(int roleId);

    // 根据 URL 获取
    Task<ApiEndpoint?> GetApiEndpointByUrlAsync(string url);
    
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<(List<ApiEndpoint> items, int totolCount)> QueryEndpointAsync(SearchPage dto);
    
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

    Task<bool> InitApiEndpointsAsync(List<ApiEndpoint> apiEndpoints);
    
    Task<List<ApiEndpoint>> GetApiPermissionsByUserIdAsync(int userId);
    
    Task<List<ApiEndpoint>> GetApisByUserIdAsync(int userId);
}