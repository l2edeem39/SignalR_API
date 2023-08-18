using SignalRAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Service.Interface
{
    public interface IChatManageService
    {
        public string addInsert();
        //public Task<List<ResponseMessageDetailModel>> GetMessageChatByNotiPolicy(RequestDetail request);
        public Task InsertChatAgnPolicy(RequestChatDetail request);
        public Task<List<ResponseApplicationModel>> GetApplicatioNo(RequestApplication request);
    }
}
