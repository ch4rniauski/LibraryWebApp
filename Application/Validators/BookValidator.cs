using Application.Abstractions.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validators
{
    public class BookValidator : AbstractValidator<CreateBookRecord>
    {
        public BookValidator()
        {
            RuleFor(b => b.ISBN).NotNull();

            RuleFor(b => b.Title)
                .NotNull()
                .Length(1, 50);

            RuleFor(b => b.Genre)
                .NotNull()
                .Length(1, 89);

            RuleFor(b => b.Description).Length(1, 250);

            RuleFor(b => b.TakenAt)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .GreaterThanOrEqualTo(new DateOnly(2000, 1, 1));

            RuleFor(b => b.DueDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .LessThan(DateOnly.FromDateTime(DateTime.Today.AddDays(30)));

            RuleFor(book => book.ISBN)
                .Must(IsValidISBNCode)
                .WithMessage("Invalid ISBN format");
        }

        public static bool IsValidISBNCode(string str)
        {
            string strRegex
                = @"^(?=(?:[^0-9]*[0-9]){10}(?:(?:[^0-9]*[0-9]){3})?$)[\d-]+$";
            Regex re = new(strRegex);
            if (re.IsMatch(str))
                return (true);
            else
                return (false);
        }
    }
}
