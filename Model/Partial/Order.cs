using System;
using System.ComponentModel;
using System.Linq;
using FluentValidation;

namespace Model
{
   public partial class Order:IDataErrorInfo
    {
        private readonly OrderValidator _validator = new OrderValidator();

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
            { var firstOrDefault = _validator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _validator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
    }

   public class OrderValidator : AbstractValidator<Order>
   {
       public OrderValidator()
       {
           RuleFor(o => o.Contractor)
               .NotEmpty()
               .WithMessage("Укажите название контрагента");

           RuleFor(o => o.EmployeeId)
               .Must(i => i>0)
               .WithMessage("Укажите автора заказа");
       }
   }
}
