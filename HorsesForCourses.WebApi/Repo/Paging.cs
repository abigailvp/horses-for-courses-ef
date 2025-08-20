using Microsoft.EntityFrameworkCore;

namespace HorsesForCourses.Paging;

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
        int skip = (request.Page - 1) * request.Size;
        return query.Skip(skip).Take(request.Size);
    }
}

public static class PagingExecution
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query, //door this kan je methode ook op query aanroepen!!
        PageRequest request,
        CancellationToken ct = default) where T : class
    {
        var total = await query.CountAsync(ct);
        var pageItems = await query
            .ApplyPaging(request)
            .AsNoTracking() // meestal gewenst voor read‑only
            .ToListAsync(ct); //cancellationToken wordt gebruikt om langlopende asynchrone bewerkingen te kunnen annuleren

        return new PagedResult<T>(pageItems, total, request.Page, request.Size);
    }
}

