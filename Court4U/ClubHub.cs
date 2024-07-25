using Microsoft.AspNetCore.SignalR;

namespace Court4U
{
    public class ClubHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ClubChanged");
        }
    }
}
