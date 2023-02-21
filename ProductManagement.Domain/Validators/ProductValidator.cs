using FluentValidation;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ManufactureDate)
                .LessThan(p => p.ExpirationDate)
                .WithMessage("A data de fabricação deve ser anterior à data de validade.");
        }
    }
}
