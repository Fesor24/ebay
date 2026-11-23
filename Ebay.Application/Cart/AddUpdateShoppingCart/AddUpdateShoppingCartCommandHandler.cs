using Ebay.Application.Abstractions.Cache;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Cart.GetShoppingCart;
using Ebay.Application.Exceptions;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Cart;
using Ebay.Domain.Products;

namespace Ebay.Application.Cart.AddUpdateShoppingCart;

internal sealed class AddUpdateShoppingCartCommandHandler : 
    ICommandHandler<AddUpdateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly ICacheService _cacheService;

    public AddUpdateShoppingCartCommandHandler(IProductRepository productRepository, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _cacheService = cacheService;
    }

    public async Task<Result<ShoppingCartResponse>> Handle(AddUpdateShoppingCartCommand request, 
        CancellationToken cancellationToken)
    {
        var products = await _productRepository
            .GetProductsByIdsAsync(request.CartItems.ConvertAll(i => i.ProductId), cancellationToken);

        ShoppingCart shoppingCart = new(request.CartId);

        foreach (var item in request.CartItems)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId) ??
                throw new NotFoundException($"Product with id {item.ProductId} not found.");

            shoppingCart.AddItem(new ShoppingCartItem(
                product.Id, 
                product.Name, 
                product.Money.Amount, 
                item.Quantity));
        }

        await _cacheService.Add(request.CartId.ToString(), shoppingCart, TimeSpan.FromDays(30));

        return new ShoppingCartResponse(shoppingCart.CartId, 
            shoppingCart.Items.ConvertAll(cart => new ShoppingCartItemResponse(
                cart.ProductId,
                cart.ProductName,
                cart.Price,
                cart.Quantity
        )));    
    }
}
