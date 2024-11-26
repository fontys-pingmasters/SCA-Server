using Microsoft.AspNetCore.SignalR;

namespace SCA_Server.Hubs;

public class MyHub : Hub
{
    public async Task GetLiveScores()
    {
        // TODO: Get all live matches function 2
        await Clients.All.SendAsync("ReceiveMessage", "Live Scores");
    }
}