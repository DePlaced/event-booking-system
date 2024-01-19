using Microsoft.AspNetCore.SignalR;

namespace BigDSignWebApp.Utilities
{
    public class EventHub : Hub
    {
        public async Task UpdateEventAvailability(int eventId, string newStatus)
        {
            await Clients.All.SendAsync("ReceiveEventUpdate", eventId, newStatus);
        }
    }
}
