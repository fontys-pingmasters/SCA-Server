using Microsoft.AspNetCore.SignalR;

namespace SCA_Server.Hubs;

public class MyHub : Hub
{
    public async Task GetLiveScores()
    {
        // TODO: Get all live matches function
        await Clients.All.SendAsync("ReceiveMessage", "Live Scores");
    }
}