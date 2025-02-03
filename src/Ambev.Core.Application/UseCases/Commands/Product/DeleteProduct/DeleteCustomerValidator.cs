using Ambev.Application.UseCases.Commands.Product.DeleteProduct;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Product.DeleteProduct;

public sealed class DeleteProductValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.id).NotEmpty();
        RuleFor(x => x.id).GreaterThan(0);
    }
}
