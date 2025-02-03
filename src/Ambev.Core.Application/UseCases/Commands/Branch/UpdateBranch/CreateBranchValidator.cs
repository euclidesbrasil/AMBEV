using Ambev.Core.Application.UseCases.Commands.Branch.UpdateBranch;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public sealed class UpdateBranchValidator : AbstractValidator<UpdateBranchRequest>
{
    public UpdateBranchValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(200);
    }
}
