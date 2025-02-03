using Ambev.Application.UseCases.Commands.Cart.DeleteCart;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public sealed class DeleteCartValidator : AbstractValidator<DeleteCartRequest>
{
    public DeleteCartValidator()
    {
        RuleFor(x => x.id).NotEmpty();
        RuleFor(x => x.id).GreaterThan(0);
    }
}
