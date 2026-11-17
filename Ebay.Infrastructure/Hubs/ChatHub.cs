using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Ebay.Infrastructure.Hubs;

public class ChatHub(ApplicationDbContext dbContext) : Hub
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task SendMessage(string userId, string message)
    {
        var newMessage = new
        {
            userId,
            message
        };

        string messageToSend = JsonSerializer.Serialize(newMessage);

        await Clients.All.SendAsync("ReceiveMessage", messageToSend);
    }

    public async Task SendChatMessage(string userId, string message)
    {
        var newMessage = new
        {
            userId,
            message
        };

        string messageToSend = JsonSerializer.Serialize(newMessage);

        Console.WriteLine(messageToSend);

        await Clients.User(userId).SendAsync("ReceiveChatMessage", messageToSend);
    }

    public override Task OnConnectedAsync()
    {
        string connectionId = Context.ConnectionId;

        string userId = Context.User?.Claims.FirstOrDefault(cl => cl.Type == JwtRegisteredClaimNames.Sub)?.Value ?? "";

        Console.WriteLine($"User ID: {Context.UserIdentifier}");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        string connectionId = Context.ConnectionId;

        Console.WriteLine($"Connection with ID: {connectionId} disconnected");

        return base.OnDisconnectedAsync(exception);
    }
}
