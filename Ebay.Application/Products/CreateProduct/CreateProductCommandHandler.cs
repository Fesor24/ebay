using Ebay.Application.Abstractions.Clock;
using Ebay.Application.Abstractions.Data;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Exceptions;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Products;

namespace Ebay.Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISearchService<ProductSearchSchema> _searchService;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, 
        IDateTimeProvider dateTimeProvider, ISearchService<ProductSearchSchema> searchService,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _searchService = searchService;
        _categoryRepository = categoryRepository;
    }
    public async Task<Result<Guid>> Handle(CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken)
            ?? throw new NotFoundException("Category not found");

        Guid productId = Guid.NewGuid();

        Product product = Product.Create(productId, request.Name, request.Description, request.Tags,
            "London",request.Price, category.Id, Currency.Usd.Code, _dateTimeProvider.UtcNow);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // todo: publish event
        ProductSearchSchema productSchema = new(
            product.Id.ToString(),
            product.Name,
            product.Description,
            product.Money.Amount,
            category.Name
            );

        // Save to search db
        await _searchService.IndexAsync(productSchema, cancellationToken);

        return productId;

    }
}
