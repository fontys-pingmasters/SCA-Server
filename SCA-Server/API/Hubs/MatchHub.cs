using System.Collections.Concurrent;
using Business.Entities;
using Business.Implementations;
using Microsoft.AspNetCore.SignalR;

namespace SCA_Server.Hubs;

public class MatchHub : Hub
{ 
    private static readonly ConcurrentDictionary<int, string> _connections = new();
    
    private readonly UserService _userService;
    private readonly MatchService _matchService;
    
    public MatchHub(UserService userService, MatchService matchService)
    {
        _userService = userService;
        _matchService = matchService;
    }
    
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var token = httpContext.Request.Query["token"];

        if (string.IsNullOrEmpty(token))
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "Error: No token provided");
            Context.Abort();
            return;
        }
        
        var userid = int.Parse(Context.UserIdentifier ?? 
                               throw new Exception("Could not find current user id in token"));
        
        _connections.AddOrUpdate(userid, Context.ConnectionId, (key, value) => Context.ConnectionId);
        
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = _connections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key.ToString();
        
        if (!string.IsNullOrEmpty(userId))
        {
            _connections.TryRemove(int.Parse(userId), out _); 
        }

        await base.OnDisconnectedAsync(exception);
    }
    
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

    public async Task SendInvitationsAsync(Match match)
    {
        var matchParticipants = new List<User>()
        {
            match.Player1,
            match.Opponent1
        };

        if (match.Player2 != null) matchParticipants.Add(match.Player2);

        if (match.Opponent2 != null) matchParticipants.Add(match.Opponent2);
        
        foreach (var participant in matchParticipants)
        {
            var connectionId = _connections.FirstOrDefault(x => x.Key == participant.Id).Value;
            if (connectionId != null)
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", match.MatchRequests.Where(x => x.Receiver.Id == participant.Id));
            }
        }
    }
    
}