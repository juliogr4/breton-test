namespace Breton.Domain.Entities.Customer.Parameters.Response;

public class GetCustomersParametersRequest
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? CPF { get; set; }
    public string? Name { get; set; }
    public int Offset => (PageNumber - 1) * PageSize;

    public GetCustomersParametersRequest(int pageSize, int pageNumber, string? cpf, string? name)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        CPF = cpf;
        Name = name;
    }
}