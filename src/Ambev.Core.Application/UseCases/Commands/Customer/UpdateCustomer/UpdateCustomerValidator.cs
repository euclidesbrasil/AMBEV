using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.UpdateCustomer;

public sealed class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Identification).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}
