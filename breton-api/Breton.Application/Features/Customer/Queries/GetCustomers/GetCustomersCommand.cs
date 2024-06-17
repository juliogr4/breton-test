using Breton.Application.Common.Pagination;
using Breton.Application.Features.Customer.DTO.Request;
using Breton.Application.Features.Customer.DTO.Response;
using MediatR;

namespace Breton.Application.Features.Customer.Queries.GetCustomers;

public record GetCustomersCommand(GetCustomersRequest GetCustomersRequest) : IRequest<PageResponse<CustomerResponse>>;
