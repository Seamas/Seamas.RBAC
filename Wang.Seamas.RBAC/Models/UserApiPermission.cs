namespace Wang.Seamas.RBAC.Models;

public class UserApiPermission
{
    public int UserId { get; set; }
    public int ApiEndpointId { get; set; }
    public bool IsAllowed { get; set; } // true=允许，false=拒绝（覆盖角色）
}