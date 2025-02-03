using Ambev.Core.Application.UseCases.Commands.Sale.CancelSale;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Sale.CancelSale;

public sealed class CancelSaleValidator : AbstractValidator<CancelSaleRequest>
{
    public CancelSaleValidator()
    {
        RuleFor(x => x.id).NotEmpty();
        RuleFor(x => x.id).GreaterThan(0);
    }
}
