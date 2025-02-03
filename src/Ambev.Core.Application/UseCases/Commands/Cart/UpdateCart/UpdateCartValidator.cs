using Ambev.Application.UseCases.Commands.Cart.UpdateCart;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public sealed class UpdateCartValidator : AbstractValidator<UpdateCartRequest>
{
    public UpdateCartValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Id).GreaterThan(0);
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
