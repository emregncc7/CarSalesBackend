using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
   public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MinimumLength(2);
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Price).GreaterThanOrEqualTo(10).When(p => p.BrandId == 1);
            RuleFor(p => p.Name).Must(NotStartWithĞ).WithMessage("Ürünler ğ harfi ile başlamamalı");
        }
        private bool NotStartWithĞ(string arg)
        {
            return !arg.StartsWith("ğ");
        }
    }

}
