namespace Breton.Domain.Common.Pagination;

public class PaginatedList<T>
{
    public int TotalItems { get; set; }
    public IEnumerable<T> Data { get; set; }

    public PaginatedList(int totalItems, IEnumerable<T> data)
    {
        TotalItems = totalItems;
        Data = data;
    }
}