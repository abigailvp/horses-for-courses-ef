namespace HorsesForCourses.Repo;

public sealed record PageRequest(int PageNumber = 1, int PageSize = 25)
{
    public int Page => PageNumber < 1 ? 1 : PageNumber;
    public int Size => PageSize is < 1 ? 1 : (PageSize > 50 ? 50 : PageSize);
}

public sealed record PagedResult<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}

public static class QueryablePagingExtensions
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PageRequest request)
    {
        if (!query.Expression.ToString().Contains("OrderBy"))
            throw new InvalidOperationException("Apply an OrderBy before paging to ensure stable results.");

        int skip = (request.Page - 1) * request.Size;
        return query.Skip(skip).Take(request.Size);
    }
}
