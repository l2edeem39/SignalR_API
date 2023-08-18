using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRAPI.HubSignalr;
using SignalRAPI.Model;
using SignalRAPI.Service.Interface;
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
        private IChatManageService _chatService;
        private readonly IHubContext<SignalrHub> _hub;
        public ChatHubController(IHubContext<SignalrHub> hubContext, IChatManageService chatService)
        {
            _hub = hubContext;
            _chatService = chatService;
        }

        [HttpGet]
        public async Task NewMessage(string userId, string userName, string message, string applicationNo)
        {
            var sde = new RequestChatDetail()
            {
                ApplicatioNo = applicationNo,
                UserId = userId,
                UserFullName = userName,
                Message = message
            };
            //var ss = _chatService.GetMessageChatByNotiPolicy(sde);
            await _chatService.InsertChatAgnPolicy(sde);
            //await Clients.All.SendAsync("User", userId, userName, message, applicationNo);
        }

        [HttpGet("GetApplicationNo")]
        public async Task<IActionResult> GetApplicationNo(string UserId, string UserType)
        {
            var request = new RequestApplication()
            {
                UserId = UserId,
                UserType = UserType
            };
            var result = await _chatService.GetApplicatioNo(request);
            return StatusCode(200, new {data = result });
            //return result;
        }
    }
}
    