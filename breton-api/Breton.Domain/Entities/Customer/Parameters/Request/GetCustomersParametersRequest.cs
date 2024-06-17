namespace Breton.Domain.Entities.Customer.Parameters.Request;

public class GetCustomersParametersRequest
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? CPF { get; set; }
    public string? Name { get; set; }
    public int Offset => (PageNumber - 1) * PageSize;
}