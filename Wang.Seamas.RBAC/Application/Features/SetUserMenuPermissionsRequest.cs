namespace Wang.Seamas.RBAC.Requests;

public record SetUserMenuPermissionsRequest(int UserId, Dictionary<int, bool> Permissions);