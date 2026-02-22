namespace Wang.Seamas.RBAC.Domain.Entities;

public class UserApiPermission
{
    public int UserId { get; set; }
    public int ApiEndpointId { get; set; }
}