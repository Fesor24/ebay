namespace Ebay.Domain.Products;

internal sealed class Chat
{
    // todo: inherit from auditable entity
    public Guid SenderId { get; private set; }
    public Guid RecipientId { get; private set; }
    public string Message { get; private set; }
}
