using Business.Entities;
using Microsoft.AspNetCore.SignalR;

namespace SCA_Server.Hubs;

public class MatchHub : Hub
{
    public async Task SendLiveScores(List<Match> matches)
    {
        try
        {
            await Clients.All.SendAsync("ReceiveMessage", matches);
        }
        catch(Exception e)
        {
            Console.Write(e);
        }
    }
}