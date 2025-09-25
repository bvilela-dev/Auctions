using System;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotifySvc.Hubs;

namespace NotifySvc.Consumers;

public class AuctionCreatedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<AuctionCreated>
{
    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        Console.WriteLine("--> Auction created message received");

        await hubContext.Clients.All.SendAsync("AuctionCreated", context.Message);
    }
}
