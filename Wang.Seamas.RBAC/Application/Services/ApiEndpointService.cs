using SqlSugar;
using Wang.Seamas.Queryable.Helpers;
using Wang.Seamas.RBAC.Domain.Common;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;


namespace Wang.Seamas.RBAC.Application.Services;

public class ApiEndpointService(ISqlSugarClient db) : IApiEndpointService
{
    

    public async Task<bool> UpdateApiEndpointAsync(int id, string? description = null, bool? isEnabled = null)
    {
        var update = db.Updateable<ApiEndpoint>().Where(a => a.Id == id);
        
        if (description != null) update = update.SetColumns(a => new ApiEndpoint { Description = description });
        if (isEnabled != null) update = update.SetColumns(a => new ApiEndpoint { IsEnabled = isEnabled.Value });
        
        return await update.ExecuteCommandAsync() > 0;
    }

    public async Task<List<ApiEndpoint>> GetAllApiEndpointsAsync()
        => await db.Queryable<ApiEndpoint>().ToListAsync();

    public async Task<List<ApiEndpoint>> GetApiEndpointByRoleAsync(int roleId)
    {
        return await db.Queryable<ApiEndpoint>()
            .InnerJoin<RoleApiPermission>((a, ra) => a.Id == ra.ApiEndpointId)
            .Where((a, ra) => ra.RoleId == roleId)
            .ToListAsync();
    }

    public async Task<ApiEndpoint?> GetApiEndpointByUrlAsync(string url)
        => await db.Queryable<ApiEndpoint>().Where(a => a.Url == url).FirstAsync();

    public async Task<(List<ApiEndpoint> items, int totolCount)> QueryEndpointAsync(SearchPage dto)
    {
        var queryable = db.Queryable<ApiEndpoint>();

        var expression = QueryHelper.Visit<ApiEndpoint>(dto);
        queryable = queryable.Where(expression);

        var totalCount = await queryable.CountAsync();
        var items = await queryable.ToPageListAsync(dto.PageIndex, dto.PageSize);
        return (items, totalCount);
    }


    public async Task<ApiEndpoint> GetApiEndpointByIdAsync(int id) => await db.Queryable<ApiEndpoint>().Where(a => a.Id == id).FirstAsync();
    
    public async Task<bool> CheckApiUrlAsync(int? id, string url)
    {
        var query = db.Queryable<ApiEndpoint>()
            .Where(x => x.Url == url);
        if (id != null)
        {
            query = query.Where(a => a.Id != id);
        }
        return !await query.AnyAsync();
    }

    public async Task<bool> CreateApiEndpointAsync(ApiEndpoint endpoint) => await db.Insertable<ApiEndpoint>(endpoint).ExecuteCommandAsync() > 0;

    public async Task<bool> UpdateApiEndpointAsync(ApiEndpoint endpoint) =>
        await db.Updateable<ApiEndpoint>(endpoint)
            .UpdateColumns(
                nameof(ApiEndpoint.Url), 
                nameof(ApiEndpoint.ApiGroup), 
                nameof(ApiEndpoint.Description)
            )
            .ExecuteCommandAsync() > 0;

    public async Task<bool> EnableApiEndpointAsync(int id, bool enabled) =>
        await db.Updateable<ApiEndpoint>()
            .SetColumns(x => x.IsEnabled, enabled)
            .Where(a => a.Id == id)
            .ExecuteCommandAsync() > 0;

    public async Task<bool> DeleteApiEndpointAsync(int id) =>
        await db.Deleteable<ApiEndpoint>()
            .Where(x => x.Id == id)
            .ExecuteCommandAsync() > 0;
    
    public async Task<bool> InitApiEndpointsAsync(List<ApiEndpoint> apiEndpoints)
    {
        var existEndpointsFromDb = await db.Queryable<ApiEndpoint>().Select(item => item.Url).ToListAsync();
        var existEndpoints = new HashSet<string>(existEndpointsFromDb);
        var list = apiEndpoints.Where(item => !existEndpoints.Contains(item.Url))
            .ToList();
        
        if (list.Any())
        {
            await db.Insertable<ApiEndpoint>(list).ExecuteCommandAsync();
        }
        
        return true;
    }

    public async Task<List<ApiEndpoint>> GetApiPermissionsByUserIdAsync(int userId)
    {
        return await db.Queryable<ApiEndpoint>()
            .InnerJoin<UserApiPermission>((a, ua) => a.Id == ua.ApiEndpointId && ua.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<ApiEndpoint>> GetApisByUserIdAsync(int userId)
    {
        return await db.Queryable<ApiEndpoint>()
            .InnerJoin<RoleApiPermission>((a, ra) => a.Id == ra.ApiEndpointId)
            .InnerJoin<UserRole>((a, ra, ur) => ra.RoleId == ur.RoleId && ur.UserId == userId)
            .Distinct()
            .ToListAsync();
    }
}