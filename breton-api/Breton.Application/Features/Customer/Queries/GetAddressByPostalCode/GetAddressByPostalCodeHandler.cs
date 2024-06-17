using Breton.Application.Features.Customer.DTO.Response;
using Breton.Domain.Entities.Customer.ValueObjects;
using Breton.Domain.Repository;
using FluentValidation;
using MediatR;

namespace Breton.Application.Features.Customer.Queries.GetAddressByPostalCode;

public class GetAddressByPostalCodeHandler : IRequestHandler<GetAddressByPostalCodeCommand, AddressResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<GetAddressByPostalCodeCommand> _validator;

    public GetAddressByPostalCodeHandler(ICustomerRepository customerRepository, IValidator<GetAddressByPostalCodeCommand> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<AddressResponse> Handle(GetAddressByPostalCodeCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var address = await _customerRepository.GetAddressByPostalCode(request.PostalCode);
        if(address is null) throw new Exception("The cep was not found");
        return ConvertAddress(address.Value);
    }

    private AddressResponse ConvertAddress(AddressBR addressBR)
    {
        var newAddress = new AddressResponse
        {
            Street = addressBR.Logradouro,
            City = addressBR.UF,
            Complement = addressBR.Complemento,
            DDD = addressBR.DDD,
            Gia = addressBR.Gia,
            Ibge = addressBR.Ibge,
            Neighborhood = addressBR.Bairro,
            PostalCode = addressBR.CEP,
            Siafi = addressBR.Siafi,
            State = addressBR.Localidade
        };

        return newAddress;
    }
}