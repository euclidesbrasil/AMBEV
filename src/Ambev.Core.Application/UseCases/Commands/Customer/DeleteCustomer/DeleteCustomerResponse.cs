using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.Customer.DeleteCustomer;

public class DeleteCustomerResponse : CustomerDTO
{
    public int Id { get; set; }
}