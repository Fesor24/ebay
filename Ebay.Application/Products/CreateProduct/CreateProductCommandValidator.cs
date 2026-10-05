using FluentValidation;

namespace Ebay.Application.Products.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(prd => prd.Price > 0);

        RuleFor(prd => prd.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(prd => prd.Description)
            .NotNull()
            .NotEmpty();
    }
}
