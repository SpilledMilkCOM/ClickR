using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRDemo.Models;

namespace SignalRDemo.Hubs
{
    /// <summary>
    /// This class contains the methods for the client to call.
    /// </summary>
    [HubName("GameHub")]  // If this is not named, then camel casing (gameHub) will be used in the JavaScript
    public class GameHub : Hub
    {
        [HubMethodName("ClickIt")]
        public void ClickIt(string userName)
        {
            var player = GameState.Instance.GetPlayer(userName);

            if (player != null)
            {
                player.Clicked();

                // This seems chatty.  Might want to create specific reponses to do certain things to reduce the chatter and total bandwidth.
                // When sending data back you should consider HOW MUCH you're sending back.  Sending the entire player could be overkill.

                Clients.Caller.refreshPlayerInfo(player);
                Clients.All.refreshGameInfo(GameState.Instance.GameInfo);
            }
        }

        /// <summary>
        /// Called on page load, the player may already be connected to the hub.
        /// </summary>
        /// <param name="userName"></param>
        [HubMethodName("ConnectPlayer")]
        public void ConnectPlayer(string userName)
        {
            var player = GameState.Instance.GetPlayer(userName);

            if (player == null)
            {
                player = GameState.Instance.CreatePlayer(userName);

                player.ConnectionId = Context.ConnectionId;
                Clients.Caller.name = player.Name;
                Clients.Caller.hash = player.Hash;
            }

            Clients.Caller.refreshPlayerInfo(player);
            Clients.All.refreshGameInfo(GameState.Instance.GameInfo);
        }

        [HubMethodName("ImReady")]
        public void ImReady(string userName)
        {
            var player = GameState.Instance.GetPlayer(userName);

            if (player != null)
            {
                player.IsReady = !player.IsReady;

                Clients.Caller.refreshReadyState(player.IsReady);
            }
        }
    }
}