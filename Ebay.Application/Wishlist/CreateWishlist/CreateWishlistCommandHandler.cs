using Ebay.Application.Abstractions.Clock;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Exceptions;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Products;
using Ebay.Domain.Wishlist;
using WishlistDomain = Ebay.Domain.Wishlist.Wishlist;

namespace Ebay.Application.Wishlist.CreateWishlist;

internal sealed class CreateWishlistCommandHandler : ICommandHandler<CreateWishlistCommand, Guid>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateWishlistCommandHandler(IWishlistRepository wishlistRepository, IProductRepository productRepository,
        IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _wishlistRepository = wishlistRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken) ??
            throw new NotFoundException("Product not found");

        if(product.UserId == request.UserId)
            throw new BadRequestException("You cannot add your own product to wishlist");

        var wishlist = WishlistDomain.Create(Guid.NewGuid(),request.UserId, 
            request.ProductId, _dateTimeProvider.UtcNow);

        _wishlistRepository.Add(wishlist);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(wishlist.Id);
    }
}
