using Ebay.Domain.Abstractions;

namespace Ebay.Application.Abstractions.Order;

public interface IOrderService : IScopedService
{
    Task<Guid> CreateAsync(string cartId, string buyerEmail, Guid deliveryMethodId,
        string street, string city, string state, string postCode);
}
