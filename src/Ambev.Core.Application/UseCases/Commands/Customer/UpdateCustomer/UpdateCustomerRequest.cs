using Ambev.Core.Application.UseCases.DTOs;
using MediatR;

namespace Ambev.Application.UseCases.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerRequest : CustomerDTO, IRequest<UpdateCustomerResponse>
    {
        public int Id { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
