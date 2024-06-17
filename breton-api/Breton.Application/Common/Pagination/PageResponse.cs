namespace Breton.Application.Common.Pagination;

public class PageResponse<T>
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; }

    public PageResponse(int totalItems, int pageSize, int pageNumber, IEnumerable<T> data)
    {
        TotalItems = totalItems;
        PageSize = pageSize;
        PageNumber = pageNumber;
        Data = data;
        TotalPages = (int)Math.Ceiling(TotalItems / (double)PageNumber);
    }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber > TotalPages;
}