using Ambev.Application.UseCases.Commands.Customer.DeleteCustomer;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public sealed class DeleteCustomerValidator : AbstractValidator<DeleteCustomerRequest>
{
    public DeleteCustomerValidator()
    {
        RuleFor(x => x.id).NotEmpty();
        RuleFor(x => x.id).GreaterThan(0);
    }
}
