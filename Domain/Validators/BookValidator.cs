using Domain.Abstractions.Records;
using FluentValidation;

namespace Domain.Validators
{
    public class BookValidator : AbstractValidator<CreateBookRecord>
    {
        public BookValidator()
        {
            RuleFor(b => b.ISBN).NotNull();

            RuleFor(b => b.Title)
                .NotNull()
                .Length(1, 50);

            RuleFor(b => b.Genre).Length(1, 89);

            RuleFor(b => b.Description).Length(1, 100);

            RuleFor(b => b.AuthorFirstName).Length(1, 30);
            RuleFor(b => b.AuthorSecondName).Length(1, 30);

            RuleFor(b => b.TakenAt)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .GreaterThanOrEqualTo(new DateOnly(2000, 1, 1));

            RuleFor(b => b.DueDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .LessThan(DateOnly.FromDateTime(DateTime.Today.AddDays(30)));
        }
    }
}
