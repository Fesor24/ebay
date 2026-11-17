using System.Text.Json;
using Ebay.Application.Abstractions.Clock;
using Ebay.Domain.Products;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Ebay.Infrastructure.Hubs;

public class ChatHub(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : Hub
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

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

    public async Task SendChatMessage(string recipientUserId, string productId, string message)
    {
        var newMessage = new
        {
            recipientUserId,
            message
        };

        string messageToSend = JsonSerializer.Serialize(newMessage);

        // cache product and users
        Product? product = await _dbContext.Set<Product>()
            .FirstOrDefaultAsync(pr => pr.Id == Guid.Parse(productId));

        if (product is null)
            return;

        Chat chat = Chat.Create(
            Guid.NewGuid(),
            Guid.Parse(Context.UserIdentifier!),
            Guid.Parse(recipientUserId),
            product.Id,
            message,
            _dateTimeProvider.UtcNow
            );

        await _dbContext.Set<Chat>().AddAsync(chat);

        await Clients.User(recipientUserId).SendAsync("ReceiveChatMessage", messageToSend);
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
