using System;
using System.Globalization;
using Microsoft.AspNet.SignalR;

namespace OnlineAuction.Hubs
{
    public class AuctionHub : Hub
    {
        internal static void NotifyAll(decimal price, DateTime time)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();
            context.Clients.All.notify(price.ToString("F2", new CultureInfo("en-US")), time.ToString("yyyy-MM-dd HH:mm"));
        }
    }
}