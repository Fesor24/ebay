using Ebay.Domain.Abstractions;
using Ebay.Domain.Users;

namespace Ebay.Domain.Products;

public sealed class Chat : AuditableEntity
{
    private Chat()
    {
        
    }
    public Chat(Guid id) : base(id)
    {
        
    }
    public Guid SenderId { get; private set; }
    public User Sender { get; private set;  }
    public Guid RecipientId { get; private set; }
    public User Recipient {  get; private set; }
    public Guid ProductId { get; private set;  }
    public Product Product { get; private set; }
    public string Message { get; private set; }

    public static Chat Create(Guid chatId, Guid senderId, Guid recipientId,
        Guid productId, string message, DateTime createdAtUtc)
    {
        Chat chat =  new (chatId)
        {
            SenderId = senderId,
            RecipientId = recipientId,
            ProductId = productId,
            Message = message
        };

        chat.SetAudits(createdAtUtc);

        return chat;
    }
}
