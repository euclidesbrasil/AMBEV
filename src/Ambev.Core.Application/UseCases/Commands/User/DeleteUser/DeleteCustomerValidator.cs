using FluentValidation;

namespace Ambev.Application.UseCases.Commands.User.DeleteUser;

public sealed class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
