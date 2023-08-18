using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRAPI.HubSignalr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatHubController : ControllerBase
    {
        private readonly IHubContext<SignalrHub> _hub;
        public ChatHubController(IHubContext<SignalrHub> hubContext)
        {
            _hub = hubContext;
        }

        [HttpGet]
        public async Task SendMessage(string msg, string rowName)
        {
            //var context = GlobalHost.ConnectionManager.GetHubContext<SignalrHub>();
            if (rowName.ToUpper() == "CLIENT")
            {
                await _hub.Clients.All.SendAsync("ReciveMessage", "werwer", msg);
            }
            else
            {
                await _hub.Clients.All.SendAsync("User", "0211154455", msg);
            }
            //var SignalrHub = new SignalrHub();
            //SignalrHub.NewMessage("ewrwer","werwerwr");
            //return "";
        }
    }
}
    