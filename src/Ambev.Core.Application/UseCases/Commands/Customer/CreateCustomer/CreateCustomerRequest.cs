using Ambev.Core.Domain.Entities;

using MediatR;
using Ambev.Core.Domain.ValueObjects;
using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer
{
    public class CreateCustomerRequest : CustomerDTO, IRequest<CreateCustomerResponse>
    {
    }
}
