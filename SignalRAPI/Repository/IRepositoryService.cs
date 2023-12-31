﻿using SignalRAPI.Entities;
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
        public Task<List<ResponseHistoryModel>> GetHistory(RequestHistory request);
        public Task<int> UpdateChatHearAdmin(RequestFlageRead request);

        public Task<int> UpdateChatHearAgent(RequestFlageRead request);
    }
}
