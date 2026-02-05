namespace Wang.Seamas.RBAC.Models;

public class UserMenuPermission
{
    public int UserId { get; set; }
    public int MenuId { get; set; }
    public bool IsAllowed { get; set; } // true=显式允许，false=显式拒绝
}