namespace Wang.Seamas.RBAC.Requests.User;

public record SearchUserRequest(int? PageIndex, int? PageSize, string? Username, string? Nickname, string? Email) : PagedRequest(PageIndex, PageSize);