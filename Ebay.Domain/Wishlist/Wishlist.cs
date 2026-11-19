using Ebay.Domain.Abstractions;
using Ebay.Domain.Products;
using Ebay.Domain.Users;

namespace Ebay.Domain.Wishlist;

public sealed class Wishlist : Entity
{
    private Wishlist()
    {
        
    }
    public Wishlist(Guid wishlistId) : base(wishlistId)
    {
    }

    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Wishlist Create(Guid wishlistId, Guid userId, Guid productId, DateTime createdAt)
    {
        return new Wishlist(wishlistId)
        {
            UserId = userId,
            ProductId = productId,
            CreatedAt = createdAt
        };
    }
}
