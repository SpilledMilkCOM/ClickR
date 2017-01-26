using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRDemo.Hubs
{
    // If this is not named, then camelCasing will be used in the JavaScript
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            // NOTE: The method name is camelCased
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}