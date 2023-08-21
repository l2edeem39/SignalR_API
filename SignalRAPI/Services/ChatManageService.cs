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

        public async Task<List<ResponseApplicationModel>> GetApplicationNo(RequestApplication request)
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

        public async Task<List<ResponseHistoryModel>> GetHistory(RequestHistory request)
        {
            try
            {
                var result = await _repo.GetHistory(request);
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
                RequestFlageRead flagRead = new RequestFlageRead();
                var result = await _repo.InsertChatDetail(request);
                if (request.UserType.ToUpper() == "ADMIN")
                {
                    flagRead.ApplicatioNo = request.ApplicatioNo;
                    flagRead.UserType = request.UserType;
                    flagRead.Flag = true;
                    var resultAdmin = await _repo.UpdateChatHearAgent(flagRead);
                }
                else
                {
                    flagRead.ApplicatioNo = request.ApplicatioNo;
                    flagRead.UserType = request.UserType;
                    flagRead.Flag = true;
                    var resultAdmin = await _repo.UpdateChatHearAdmin(flagRead);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateChatAgnPolicyRead(RequestFlageRead request)
        {
            try
            {
                if (request.UserType.ToUpper() == "ADMIN")
                {
                    request.ApplicatioNo = request.ApplicatioNo;
                    request.Flag = false;
                    var resultAdmin = await _repo.UpdateChatHearAdmin(request);
                }
                else
                {
                    request.ApplicatioNo = request.ApplicatioNo;
                    request.Flag = false;
                    var resultAdmin = await _repo.UpdateChatHearAgent(request);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
