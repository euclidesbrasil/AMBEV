using Ambev.Core.Application.UseCases.DTOs;
using MediatR;

namespace Ambev.Application.UseCases.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerRequest : CustomerDTO, IRequest<UpdateCustomerResponse>
    {
        public int Id { get; internal set; }

        public void Update(int id)
        {
            Id = id;
        }
    }
}
