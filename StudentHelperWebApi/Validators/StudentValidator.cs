using FluentValidation;
using StudentHelperWebApi.Data.Entities;
using StudentHelperWebApi.POCO;
using System.Linq;

namespace StudentHelperWebApi.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.FirstName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("{PropertyName} is required")
                                     .Length(1, 50).WithMessage("{PropertyName} must be {MaxLength} characters or less, entered {TotalLength}")
                                     .Must(CheckTextOnly).WithMessage("{PropertyName} contains invalid characters");


            RuleFor(s => s.LastName).Cascade(CascadeMode.Stop)
                                    .NotNull().NotEmpty().WithMessage("{PropertyName} is required")
                                    .Length(1, 50).WithMessage("{PropertyName} must be {MaxLength} characters or less, entered {TotalLength}");

            RuleFor(s => s.Email).Cascade(CascadeMode.Stop)
                                 .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                                 .Length(1, 60).WithMessage("{PropertyName} must be {MaxLength} characters or less, entered {TotalLength}")
                                 .EmailAddress().WithMessage("Invalid {PropertyName}, entered {TotalLength}");

            RuleFor(s => s.RegistrationDate).NotNull().NotEmpty().WithMessage("Enter a registration date");
        }

        protected bool CheckTextOnly(string str)
        {
            str = str.Replace(" ", "");
            str = str.Replace("-", "");
            return str.All(char.IsLetter);
        }
    }
}
