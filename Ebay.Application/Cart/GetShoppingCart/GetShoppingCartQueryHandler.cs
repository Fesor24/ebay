using Ebay.Application.Abstractions.Cache;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Cart;

namespace Ebay.Application.Cart.GetShoppingCart;

internal sealed class GetShoppingCartQueryHandler : IQueryHandler<GetShoppingCartQuery, ShoppingCartResponse>
{
    private readonly ICacheService _cacheService;

    public GetShoppingCartQueryHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<Result<ShoppingCartResponse>> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cacheService.GetItem<ShoppingCart>(request.CartId.ToString());

        if (cart is null)
            return new ShoppingCartResponse(request.CartId.ToString(), []);

        return new ShoppingCartResponse(
            CartId: cart.CartId,
            Items: cart.Items
                .ConvertAll(item =>
                    new ShoppingCartItemResponse(item.ProductId, item.ProductName, item.Price, item.Quantity)));
    }
}
