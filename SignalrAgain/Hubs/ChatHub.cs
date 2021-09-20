using Microsoft.AspNetCore.SignalR;
using SignalrAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrAgain.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(Messages message) =>
           await Clients.All.SendAsync("receiveMessage", message);
    }
}
