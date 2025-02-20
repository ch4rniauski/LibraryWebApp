﻿using Application.Abstractions.Requests;
using FluentValidation;

namespace Application.Validators
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
        }
    }
}
