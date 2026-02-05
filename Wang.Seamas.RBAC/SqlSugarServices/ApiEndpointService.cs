using SqlSugar;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;

namespace Wang.Seamas.RBAC.SqlSugarServices;

public class ApiEndpointService(ISqlSugarClient db) : IApiEndpointService
{
    public async Task<int> UpsertApiEndpointAsync(string url, string? description = null, bool isEnabled = true)
    {
        var existing = await db.Queryable<ApiEndpoint>().Where(a => a.Url == url).FirstAsync();
        if (existing != null)
        {
            existing.Description = description;
            existing.IsEnabled = isEnabled;
            await db.Updateable(existing).ExecuteCommandAsync();
            return existing.Id;
        }
        else
        {
            return await db.Insertable(new ApiEndpoint
            {
                Url = url,
                Description = description,
                IsEnabled = isEnabled
            }).ExecuteReturnIdentityAsync();
        }
    }

    public async Task BulkUpsertApiEndpointsAsync(IEnumerable<(string Url, string? Description)> endpoints)
    {
        await db.Ado.UseTranAsync(async () =>
        {
            foreach (var (url, desc) in endpoints)
                await UpsertApiEndpointAsync(url, desc);
        });
    }

    public async Task<bool> UpdateApiEndpointAsync(int id, string? description = null, bool? isEnabled = null)
    {
        var update = db.Updateable<ApiEndpoint>().Where(a => a.Id == id);
        
        if (description != null) update = update.SetColumns(a => new ApiEndpoint { Description = description });
        if (isEnabled != null) update = update.SetColumns(a => new ApiEndpoint { IsEnabled = isEnabled.Value });
        
        return await update.ExecuteCommandAsync() > 0;
    }

    public async Task<List<ApiEndpoint>> GetAllApiEndpointsAsync()
        => await db.Queryable<ApiEndpoint>().ToListAsync();

    public async Task<ApiEndpoint?> GetApiEndpointByUrlAsync(string url)
        => await db.Queryable<ApiEndpoint>().Where(a => a.Url == url).FirstAsync();
}