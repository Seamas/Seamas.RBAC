namespace Wang.Seamas.RBAC.Dtos;

public class SearchPageDto
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