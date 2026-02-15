namespace Wang.Seamas.RBAC.Domain.Common;

public class SearchPage
{
    public int PageIndex
    {
        get;
        set => field = value < 1 ? 1 : value;
    } = 1;

    public int PageSize
    {
        get;
        set => field = value < 1 ? 1 : value;
    } = 10;
}