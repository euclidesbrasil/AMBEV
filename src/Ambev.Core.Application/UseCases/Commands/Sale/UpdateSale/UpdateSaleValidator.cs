﻿using Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale;
using FluentValidation;

namespace Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale;

public sealed class UpdateSaleValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleValidator()
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
