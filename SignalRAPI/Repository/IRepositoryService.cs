using SignalRAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Repository
{
    public interface IRepositoryService
    {
        public Task<List<NotiPolicy>> GetNotiPolicyAsync(string ApplicationNo);
    }
}
