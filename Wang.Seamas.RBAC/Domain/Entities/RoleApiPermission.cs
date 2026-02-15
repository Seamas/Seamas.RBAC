namespace Wang.Seamas.RBAC.Domain.Entities;

public class RoleApiPermission
{
    public int RoleId { get; set; }
    public int ApiEndpointId { get; set; }
}