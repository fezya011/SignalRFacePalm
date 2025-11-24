using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Input;
using WpfAppSlapingPoping.VMTools;
using System.Windows;
using WpfAppSlapingPoping.View;

namespace WpfAppSlapingPoping.ViewModel
{
    class LoginWindowViewModel : BaseVM
    {
        private string name;
        static HubConnection client;

        public string Name 
        { 
            get => name;
            set
            {
                name = value;
                Signal();
            }
        }

        public ICommand JoinGameButton { get; set; }

        public LoginWindowViewModel(LoginWindow loginWindow)
        {
            JoinGameButton = new CommandVM(async () =>
            {
                bool result = await CheckName();
                if (!result)
                {
                    MessageBox.Show("Такое имя пользователя уже используется");
                    return;
                }

                else
                {
                    GameWindow gameWindow = new GameWindow();
                    gameWindow.Show();
                    loginWindow.Close();
                }
            });

            

        }




        public async Task<bool> CheckName()
        {
            
            client = new HubConnectionBuilder()
                       .WithUrl("http://localhost:5079/game")
                       .WithAutomaticReconnect()
                       .Build();
            try
            {
                await client.StartAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show($"{e.Message}");
            }

            var answer = false;
            answer = await client.InvokeAsync<bool>("Registration", Name);
            return answer;
        }
    }
}
