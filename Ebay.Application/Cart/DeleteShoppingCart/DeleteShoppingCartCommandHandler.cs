using Ebay.Application.Abstractions.Cache;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Cart;

namespace Ebay.Application.Cart.DeleteShoppingCart;

internal sealed class DeleteShoppingCartCommandHandler : ICommandHandler<DeleteShoppingCartCommand, bool>
{
    private readonly ICacheService _cacheService;

    public DeleteShoppingCartCommandHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
    public async Task<Result<bool>> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cacheService.GetItem<ShoppingCart>(request.CartId.ToString());

        if (cart is null) return true;

        await _cacheService.RemoveItem(request.CartId.ToString());

        return true;
    }
}
