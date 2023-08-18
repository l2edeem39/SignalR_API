using SignalRAPI.Entities;
using SignalRAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Repository
{
    public interface IRepositoryService
    {
        //public Task<List<NotiPolicy>> GetNotiPolicyAsync(string ApplicationNo);
        public Task<int> InsertChatDetail(RequestChatDetail request);
        public Task<List<ResponseApplicationModel>> GetApplicatioNo(RequestApplication request);
    }
}
