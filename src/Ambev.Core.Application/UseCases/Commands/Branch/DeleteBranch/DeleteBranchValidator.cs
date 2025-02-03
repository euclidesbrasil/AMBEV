using Ambev.Core.Application.UseCases.Commands.Branch.DeleteBranch;
using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public sealed class DeleteBranchValidator : AbstractValidator<DeleteBranchRequest>
{
    public DeleteBranchValidator()
    {
        RuleFor(x => x.id).NotEmpty();
        RuleFor(x => x.id).GreaterThan(0);
    }
}
