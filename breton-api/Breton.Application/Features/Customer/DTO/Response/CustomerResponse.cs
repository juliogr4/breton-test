namespace Breton.Application.Features.Customer.DTO.Response;

public class CustomerResponse 
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public string Name { get; set; } = String.Empty;
    public string CPF { get; set; }
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; }
    public AddressResponse Address { get; set; }
}