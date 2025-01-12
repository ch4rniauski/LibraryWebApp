using Application.Abstractions.Requests;
using FluentValidation;

namespace Domain.Validators
{
    public class AuthorValidator : AbstractValidator<CreateAuthorRecord>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.FirstName)
                .NotNull()
                .Length(1, 30);

            RuleFor(a => a.SecondName)
                .NotNull()
                .Length(1, 30);

            RuleFor(a => a.Country)
                .NotNull()
                .Length(1, 168);

            RuleFor(a => a.BirthDate)
                .NotNull()
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .GreaterThanOrEqualTo(new DateOnly(1908, 5, 23));
        }
    }
}
