namespace Wang.Seamas.RBAC.Requests.Auth;

public record ChangePasswordRequest(string OldPassword, string NewPassword);