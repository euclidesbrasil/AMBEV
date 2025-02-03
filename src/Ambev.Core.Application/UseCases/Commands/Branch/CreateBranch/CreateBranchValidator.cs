using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public sealed class CreateBranchValidator : AbstractValidator<CreateBranchRequest>
{
    public CreateBranchValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(200);
    }
}
