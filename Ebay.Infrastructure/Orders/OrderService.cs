using Ebay.Application.Abstractions.Cache;
using Ebay.Application.Abstractions.Order;
using Ebay.Application.Exceptions;
using Ebay.Domain.Cart;
using Ebay.Domain.Orders;
using Ebay.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ebay.Infrastructure.Orders;

internal class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public OrderService(ApplicationDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }
    public async Task<Guid> CreateAsync(string cartId, string buyerEmail, Guid deliveryMethodId, 
        string street, string city, string state, string postCode)
    {
        DeliveryMethod? deliveryMethod = _dbContext.Set<DeliveryMethod>()
            .FirstOrDefault(x => x.Id == deliveryMethodId) ??
            throw new NotFoundException("Delivery method not found");

        ShoppingCart? shoppingCart = await _cacheService.GetItem<ShoppingCart>(cartId) ??
            throw new NotFoundException("Shopping cart not found");

        Order order = Order.Create(Guid.NewGuid(), street, city, state, postCode, deliveryMethod, buyerEmail);

        List<Guid> productIds = [.. shoppingCart.Items
            .Select(prd => prd.ProductId)
            .Cast<Guid>()];

        IReadOnlyList<Product> products = await _dbContext.Set<Product>()
            .Where(prd => productIds.Contains(prd.Id))
            .ToListAsync();

        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            foreach (var item in shoppingCart.Items)
            {
                Product? product = products.FirstOrDefault(prd => prd.Id == item.ProductId) ??
                    throw new NotFoundException($"Product with id {item.ProductId} not found");

                if(product.Status != ProductStatus.Listed)
                    throw new InvalidOperationException($"Product with id {item.ProductId} has been sold");

                product.UpdateStatus(ProductStatus.Sold);

                order.AddItem(Guid.NewGuid(), product.Name, product.Money.Amount, product.Id);
            }

            _dbContext.Set<Product>().UpdateRange(products);

            await _dbContext.Set<Order>().AddAsync(order);

            await _dbContext.SaveChangesAsync();

            await _cacheService.RemoveItem(cartId);

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return order.Id;
    }
}
