using Domain.Abstractions.Records;
using FluentValidation;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator <RegisterUserRecord>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotNull()
                .MinimumLength(8);

            RuleFor(u => u.Login)
                .NotNull()
                .MinimumLength(3);

            RuleFor(u => u.IsAdmin).NotEmpty();
        }
    }
}
