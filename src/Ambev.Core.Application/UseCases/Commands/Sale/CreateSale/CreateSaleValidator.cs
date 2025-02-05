using Ambev.Core.Application.UseCases.Commands.Sale.CreateSale;
using FluentValidation;

namespace Ambev.Core.Application.UseCases.Commands.Sale.CreateSale;

public sealed class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleValidator()
    {

        RuleFor(s => s.SaleDate)
            .NotEmpty().WithMessage("Sale date is required.");

        RuleFor(s => s.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");
       

        RuleFor(s => s.BranchId)
            .NotEmpty().WithMessage("Branch ID is required.");

        RuleFor(s => s.IsCancelled)
            .NotNull().WithMessage("Cancellation status is required.");
    }
}
