using Ambev.Application.UseCases.Commands.Cart.CreateCart;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public sealed class CreateCartValidator : AbstractValidator<CreateCartRequest>
{
    public CreateCartValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Products).Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .Must(x => x.Count > 0)
            .WithMessage("Products must have at least one item")
            .Must(x => x.All(p => p.ProductId > 0))
            .WithMessage("Product Id must be greater than 0")
            .Must(x => x.All(p => p.Quantity > 0))
            .WithMessage("Quantity must be greater than 0");
    }
}
