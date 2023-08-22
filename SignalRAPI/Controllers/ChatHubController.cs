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
        public async Task NewMessage(string userId, string userName, string message, string applicationNo, string userType)
        {
            var insert = new RequestChatDetail()
            {
                ApplicatioNo = applicationNo,
                UserId = userId,
                UserFullName = userName,
                Message = message,
                UserType = userType
            };
            //var ss = _chatService.GetMessageChatByNotiPolicy(sde);
            await _chatService.InsertChatAgnPolicy(insert);
            //await Clients.All.SendAsync("User", userId, userName, message, applicationNo);
        }

        [HttpGet("UpdateRead")]
        public async Task UpdateRead(string applicationNo, string userType)
        {
            var update = new RequestFlageRead()
            {
                ApplicatioNo = applicationNo,
                UserType = userType
            };
            //var ss = _chatService.GetMessageChatByNotiPolicy(sde);
            await _chatService.UpdateChatAgnPolicyRead(update);
            //await Clients.All.SendAsync("User", userId, userName, message, applicationNo);
        }

        [HttpGet("GetApplicationNo")]
        public async Task<IActionResult> GetApplicationNo(string UserId, string UserType, int PageScroll)
        {
            var request = new RequestApplication()
            {
                UserId = UserId,
                UserType = UserType,
                PageScroll = PageScroll
            };
            var result = await _chatService.GetApplicationNo(request);
            return StatusCode(200, new {data = result });
            //return result;
        }

        [HttpGet("GetHistory")]
        public async Task<IActionResult> GetHistory(string ApplicationNo)
        {
            var request = new RequestHistory()
            {
                ApplicationNo = ApplicationNo
            };
            var result = await _chatService.GetHistory(request);
            return StatusCode(200, new { data = result });
            //return result;
        }
    }
}
    