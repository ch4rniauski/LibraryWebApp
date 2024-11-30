using FluentValidation;

namespace Domain.Models
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Id).NotNull();

            RuleFor(b => b.Title)
                .NotNull()
                .Length(1, 50);

            RuleFor(b => b.Description).Length(1, 100);

            RuleFor(b => b.ISBN).NotNull();
        }
    }
}
