﻿using FluentValidation;

namespace SamuraiLegend.Application.Validations
{
    public static class ValidatorConfiguration
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotNull().WithMessage("Password is required")
                .NotEmpty()
                .MinimumLength(6).WithMessage("Password must contain at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain atleast 1 uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain atleast 1 lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain a number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");

            return options;
        }

        public static IRuleBuilder<T, string> HumanName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Name must be provided")
                .Matches("[A-Za-z]").WithMessage("Name can only contain alphabeths")
                .MinimumLength(2).WithMessage("Name is limited to a minimum of 2 characters")
                .MaximumLength(25).WithMessage("Name is limited to a maximum of 25 characters");

            return options;
        }

        public static IRuleBuilder<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(@"^\+?([0-9]{3})?\)?0?([0-9]{10})$")
                .WithMessage("Phone number format should either be +234xxxxxxxxxxx or 0xxxxxxxxxx")
                .OverridePropertyName("phone_number");

            return options;
        }

    }
}
