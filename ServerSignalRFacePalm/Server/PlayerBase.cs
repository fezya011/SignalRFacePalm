namespace ServerSignalRFacePalm.Server
{
    public class PlayerBase
    {
        Dictionary<string, Player> players = new();
        public bool IsExist(string name)
        { 
            return players.ContainsKey(name);
        }

        public Player Add(string name)
        {
            var player = new Player() { Name = name, HP = 10 };
            players.Add(name, player);
            return player;
        }

        public void Remove(Player player)
        {
            if (IsExist(player.Name)) 
                players.Remove(player.Name);
        }
    }
}
