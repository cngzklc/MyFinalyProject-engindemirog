using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    //Doğrulama kuralları
    //FluentValidation bir çok dil de hata mesajı veriyor. Ama özellikle farklı bir hata mesajı vermesini istersek, WithMessage("Mesaj") olarak verebiliriz.
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//içecek katagorisinden olanların UnitPrice'ı 10 dan büyük yada eşit olmalı kuralı.
            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");
            //RuleFor(p=> p.CategoryId).
        }

        private bool StartWithA(string arg) // Bu şekilde kendi metodlarımızı yada kurallarımızı da yazabiliriz.  A ile başlamalı
        {
            return arg.StartsWith("A");
        }
    }
}
