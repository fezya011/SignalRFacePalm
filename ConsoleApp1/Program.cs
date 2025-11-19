// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using ServerSignalRFacePalm.Server;

public class Program
{
    static HubConnection client;
    static Room currentRoom;
    static int status = 0;
    static string currentName;
    public async static Task Main()
    {
        await MainTask();
        while (status != 0)
        {
            string command = Console.ReadLine();
            if (status == 2 && (command == "attack" || command == "defense"))
            {
                string human = "";
                while (!currentRoom.PlayerState.ContainsKey(human))
                {
                    Console.WriteLine("Напишите цель действия:");
                    human = Console.ReadLine();
                }
                var action = new RoomAction
                {
                    ActionType = (command == "attack" ? 1 : 2),
                    Actor = currentName,
                    GroupId = currentRoom.Number,
                    Target = human
                };
                await client.SendAsync("PickAction", action);
                status = 1;
            }
        }
        try
        {
            if (client.State != HubConnectionState.Disconnected) 
                client.StopAsync();
        }
        catch { }
    }
    public static async Task MainTask()
    {
        client = new HubConnectionBuilder()
                        .WithUrl("https://localhost:7205/game")
                        .WithAutomaticReconnect()
                        .Build();
        client.On<Room>("StartRound", s =>
        {
            currentRoom = s;
            Console.WriteLine("Начало раунда!");
            Console.WriteLine("Люди в комнате:");
            foreach (var p in s.PlayerState.Values)
            {
                Console.WriteLine($"{p.Name}, hp: {p.HP}");
            }
            if (s.PlayerState.ContainsKey(currentName))
            {
                Console.WriteLine("Выберите действие: attack или defense");
                status = 2;
            }
        });

        client.On<List<string>>("PastRoundInfo", s => {
            s.ForEach(s => Console.WriteLine(s));
        });

        client.On<Player>("Winner", s => {
            Console.WriteLine("Выиграл игрок " + s.Name);
        });
        
        try
        {
            await client.StartAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        var answer = false;
        status = 1;
        while (!answer)
        {
            Console.WriteLine("Имя?");
            string name = Console.ReadLine();
            currentName = name;
            answer = await client.InvokeAsync<bool>("Registration", name);
        }
        
       
    }
}
