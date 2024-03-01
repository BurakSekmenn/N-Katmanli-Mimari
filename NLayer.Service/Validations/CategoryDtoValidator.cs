using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{ProtertyName} is required").NotNull().WithMessage("{ProtertyName} is required");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Name can not be longer than 200 characters");
        }
    }
}
