using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Product.CreateProduct;

public sealed class UpdateProductValidator : AbstractValidator<CreateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}
