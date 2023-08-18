using Microsoft.Extensions.Configuration;
using SignalRAPI.Model;
using SignalRAPI.Repository;
using SignalRAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Service
{
    public class ChatManageService : IChatManageService
    {
        public static IConfiguration _configuration;
        SqlConnection connection;
        public static string GetConnectionString()
        {
            return _configuration.GetConnectionString("DB");
        }
        public string addInsert() 
        {
            return "";
        }
        #region Inject
        private readonly IRepositoryService _repo;
        public ChatManageService(IRepositoryService repo)
        {
            _repo = repo;
        }
        #endregion
        //public async Task<List<ResponseMessageDetailModel>> GetMessageChatByNotiPolicy(RequestDetail request)
        //{
        //    try
        //    {
        //        var result = new List<ResponseMessageDetailModel>();
        //        var res = await _repo.GetNotiPolicyAsync(request.ApplicatioNo);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<List<ResponseApplicationModel>> GetApplicatioNo(RequestApplication request)
        {
            try
            {
                var result = await _repo.GetApplicatioNo(request);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertChatAgnPolicy(RequestChatDetail request)
        {
            try
            {
                var result = await _repo.InsertChatDetail(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
