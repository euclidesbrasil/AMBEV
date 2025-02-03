using FluentValidation;

namespace Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Identification).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}
