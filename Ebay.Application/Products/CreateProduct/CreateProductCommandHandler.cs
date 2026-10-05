using Ebay.Application.Abstractions.Clock;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Products;

namespace Ebay.Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, 
        IDateTimeProvider dateTimeProvider)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        Guid productId = Guid.NewGuid();

        Product product = Product.Create(productId, request.Name, request.Description, request.Tags,
            "London",request.Price, "$", _dateTimeProvider.UtcNow);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return productId;

    }
}
