namespace Breton.Application.Features.Customer.DTO.Response;

public class GetCustomerResponse 
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public string Name { get; set; } = String.Empty;
    public string CPF { get; set; }
    public DateTime Birthdate { get; set; }
    public string Phone { get; set; }
}