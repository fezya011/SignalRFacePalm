using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppSlapingPoping.VMTools;

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

        public LoginWindowViewModel()
        {
            //client = new HubConnectionBuilder()
            //            .WithUrl("http://localhost:5079/game")
            //            .WithAutomaticReconnect()
            //            .Build();

        }
    }
}
