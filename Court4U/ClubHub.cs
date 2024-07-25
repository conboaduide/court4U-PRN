using Microsoft.AspNetCore.SignalR;

namespace Court4U
{
    public class ClubHub : Hub
    {
        public async Task SendClubChangedMessage()
        {
            await Clients.All.SendAsync("ClubChanged");
        }

        public async Task SendCourtChangedMessage()
        {
            await Clients.All.SendAsync("CourtChanged");
        }
    }
}
