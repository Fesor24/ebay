namespace Ebay.Infrastructure.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string recipientUserId, string message);
    Task ReceiveChatMessage(string recipientUserId, string message);
}
