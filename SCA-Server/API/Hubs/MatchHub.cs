using Business.Entities;
using Microsoft.AspNetCore.SignalR;

namespace SCA_Server.Hubs;

public class MatchHub : Hub
{
    public async Task SendLiveScores(List<Match> matches)
    {
        await Clients.All.SendAsync("ReceiveMessage", matches);
    }
}