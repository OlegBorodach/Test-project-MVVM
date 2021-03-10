using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Model
{
    public partial class Employee : IDataErrorInfo
    {
        private readonly EmployeeValidator _validator = new EmployeeValidator();

        public string Error
        {
            get
            {
                var results = _validator?.Validate(this);
                if (results != null && results.Errors.Any())
                {
                    var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                    return errors;
                }
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = _validator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _validator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
    }

    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(o => o.Surname)
                .NotEmpty()
                .WithMessage("Укажите фамилию сотрудника");
            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("Укажите имя сотрудника");
            RuleFor(o => o.Patronymic)
                .NotEmpty()
                .WithMessage("Укажите отчество сотрудника");
            RuleFor(o => o.DateOfBirth)
                .Must(time => time < DateTime.Now&&time>DateTime.Now.Subtract(TimeSpan.FromDays(365*60)))
                .WithMessage("Не верно указан дата рождения");
            RuleFor(o => o.DepartmentId)
                .Must(i => i>0)
                .WithMessage("Укажите подразделение сотрудника");
        }
    }
}
