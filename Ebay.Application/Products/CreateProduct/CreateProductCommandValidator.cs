using FluentValidation;

namespace Ebay.Application.Products.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(prd => prd.Name)
            .NotNull().WithMessage("Name can not by null")
            .NotEmpty().WithMessage("Name can not be empty");

        RuleFor(prd => prd.Description)
            .NotNull().WithMessage("Description can not be null")
            .NotEmpty().WithMessage("Description can not be empty");

        RuleFor(prd => prd.Price)
            .GreaterThan(1)
            .WithMessage("Price must be greater than one");

        RuleFor(prd => prd.CategoryId)
            .NotNull().WithMessage("Category id can not be null")
            .NotEmpty().WithMessage("Category id can not be empty");
    }
}
