using Breton.Application.Common.Pagination;
using Breton.Application.Features.Customer.DTO.Response;
using Breton.Domain.Entities.Customer.Parameters.Request;
using Breton.Domain.Repository;
using FluentValidation;
using Mapster;
using MediatR;

namespace Breton.Application.Features.Customer.Queries.GetCustomers;

public class GetCustomersCommandHandler : IRequestHandler<GetCustomersCommand, PageResponse<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<GetCustomersCommand> _validator;
    
    public GetCustomersCommandHandler(ICustomerRepository customerRepository, IValidator<GetCustomersCommand> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<PageResponse<CustomerResponse>> Handle(GetCustomersCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var parameters = request.GetCustomersRequest.Adapt<GetCustomersParametersRequest>();

        var customers = await _customerRepository.GetCustomers(parameters);

        var paginationResponse = new PageResponse<CustomerResponse>(
            customers.TotalItems, 
            request.GetCustomersRequest.PageSize,
            request.GetCustomersRequest.PageNumber,
            customers.Data.Select(x => x.Adapt<CustomerResponse>())
        );

        return paginationResponse;
    }
}