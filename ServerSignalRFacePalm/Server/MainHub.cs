using Microsoft.AspNetCore.SignalR;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ServerSignalRFacePalm.Server
{
    public class MainHub : Hub
    {
        private readonly RoomBase roomBase;
        private readonly PlayerBase playerBase;

        public MainHub(
            RoomBase roomBase, 
            PlayerBase playerBase)
        {
            this.roomBase = roomBase;
            this.playerBase = playerBase;
        }

        public async Task<bool> Registration(string name)
        {
            if (playerBase.IsExist(name))
                return false;

            var player = playerBase.Add(name);

            var groupId = roomBase.AddNewPlayerAndGetRoom(player);
            await Groups.AddToGroupAsync(this.Context.ConnectionId, groupId);

            if (roomBase.Ready(groupId))
            {
                await roomBase.StartAsync(groupId, Clients);
            }

            return true;
        }

        public async Task PickAction(RoomAction action)
        {
            await roomBase.AddRoundActionAsync(action, Clients);
        }
    }
}
