// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;

var client = new HubConnectionBuilder()
                .WithUrl("https://localhost:7205/game")
                .WithAutomaticReconnect()
                .Build();
await client.StartAsync();
Console.ReadLine();
