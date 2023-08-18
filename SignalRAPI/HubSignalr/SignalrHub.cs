using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRAPI.Model;
using SignalRAPI.Service.Interface;

namespace SignalRAPI.HubSignalr
{
    public class SignalrHub : Hub
    {
        private IChatManageService _chatService;
        public SignalrHub(IChatManageService chatService)
        {
            _chatService = chatService;
        }
        public async Task NewMessage(string userId, string userName, string message, string applicationId)
        {
            var sde = new RequestDetail()
            {
                ApplicatioNo = applicationId
            };
            var ss = _chatService.GetMessageChatByNotiPolicy(sde);
            await Clients.All.SendAsync("User", userId, userName, message, applicationId);
        }
    }
}
