using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Product.CreateProduct;

public sealed class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
    }
}
