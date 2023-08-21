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
        public async Task NewMessage(string userId, string userName, string message, string applicationNo, string userType)
        {
            var sde = new RequestChatDetail()
            {
                ApplicatioNo = applicationNo,
                UserId = userId,
                UserFullName = userName,
                Message = message,
                UserType = userType
            };
            //var ss = _chatService.GetMessageChatByNotiPolicy(sde);
            await _chatService.InsertChatAgnPolicy(sde);
            await Clients.All.SendAsync("User", userId, userName, message, applicationNo);
        }
    }
}
