using Apsiyon.Entities.Concrete;
using FluentValidation;

namespace Apsiyon.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün ismini boş geçemezsiniz");
            RuleFor(p => p.ProductName).Length(2,30).WithMessage("Ürün ismi en az 2 en fazla 30 karakterden oluşmalıdır.");
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("Ürün fiyatını boş geçemezsiniz");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1).WithMessage("Ürün fiyatı Minimum 1 TL olmalıdır.");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1).When(p=>p.CategoryId == 1);
            RuleFor(p => p.UnitsInStock).NotEmpty().WithMessage("Ürün stok durumu boş geçilemez");
            RuleFor(p => p.QuantityPerUnit).NotEmpty().WithMessage("Ürün birim fiyatını boş geçilemez");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Ürünü kategorisiz kayıt edemezsiniz");
            //RuleFor(p => p.ProductName).Must(StartWithA);
        }

        //ürün ismi bişeyle başlanmak istenirse kullanılır
        private bool StartWithA(string args)
        {
            return args.StartsWith("A");
        }
    }
}
