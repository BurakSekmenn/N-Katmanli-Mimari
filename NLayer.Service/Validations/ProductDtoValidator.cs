using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {

        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required").NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Name can not be longer than 200 characters");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stock is required");
            RuleFor(x => x.Stock).InclusiveBetween(1, Int32.MaxValue).WithMessage("Stock must be greater than 0");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(x => x.Price).InclusiveBetween(1, Int32.MaxValue).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
        }




    }
}
