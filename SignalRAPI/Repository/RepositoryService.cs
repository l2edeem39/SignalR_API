using Microsoft.EntityFrameworkCore;
using SignalRAPI.Context;
using SignalRAPI.Entities;
using SignalRAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private readonly DbContextClass _dbContext;


        public RepositoryService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        //public async Task<List<NotiPolicy>> GetNotiPolicyAsync(string ApplicationNo)
        //{
        //    var paramPolicyNumber = new SqlParameter("@PolicyNumber", PolicyNumber);
        //    var paramIdentityNumber = new SqlParameter("@IdentityNumber", IdentityNumber);
        //    var paramInsuredFirstName = new SqlParameter("@InsuredFirstName", InsuredFirstName);
        //    var paramInsuredLastName = new SqlParameter("@InsuredLastName", InsuredLastName);
        //    var result = await Task.Run(() => _dbContext.PolicyDetailByInsuredData
        //                    .FromSqlRaw(@"exec sp_TPA_VIB @PolicyNumber, @IdentityNumber, @InsuredFirstName, @InsuredLastName", paramPolicyNumber, paramIdentityNumber, paramInsuredFirstName, paramInsuredLastName)
        //                    .ToListAsync());

        //    return result;
        //}

        //public async Task<List<NotiPolicy>> GetNotiPolicyAsync(string ApplicationNo)
        //{
        //    //var result = _dbContext.NotiPolicy.ToList();

        //    return result;
        //}

        //public async Task<int> InsertLog(string Id, string IPaddress, string ApiOperation, string ReferenceCode, string PolicyNumber, string Request)
        //{
        //    _dbContextLogin.Log.Add(new Log()
        //    {
        //        Id = Guid.Parse(Id),
        //        IPaddress = IPaddress,
        //        ApiOperation = ApiOperation,
        //        CreateDate = DateTime.Now,
        //        ReferenceCode = ReferenceCode,
        //        PolicyNumber = PolicyNumber,
        //        Request = Request
        //    });
        //    return _dbContextLogin.SaveChanges();
        //}

        //public async Task<int> InsertLogDetail(string Id, string Event, string StatusCode, string Message, int Sequence)
        //{
        //    //var sequence = GetSequenceLogDeatilAsync(Id) + 1;
        //    _dbContextLogin.LogDetail.Add(new LogDetail()
        //    {
        //        Id = new Guid(),
        //        Sequence = Sequence,
        //        Event = Event,
        //        StatusCode = StatusCode,
        //        Message = Message,
        //        CreateDate = DateTime.Now,
        //        Log_Id = Guid.Parse(Id)
        //    });
        //    return _dbContextLogin.SaveChanges();
        //}

        public async Task<List<ResponseApplicationModel>> GetApplicatioNo(RequestApplication request)
        {
            List<ResponseApplicationModel> result = new List<ResponseApplicationModel>();
            if (request.UserType.ToUpper() == "ADMIN")
            {
                var dataHead = _dbContext.ChatAgnPolicy;
                var dataDetail = _dbContext.ChatAgnPolicyDetail;

                var queryGroupMax = 
                    from Detail in dataDetail
                    group Detail by Detail.RefId into dataDetailGroup
                    select new
                    {
                        Key = dataDetailGroup.Key,
                        LastDate = (
                            from Detail2 in dataDetailGroup
                            select Detail2.CreateDate
                        ).Max()
                    };


                var resultGroup =  from e in dataHead
                                  join d in dataDetail
                                         on e.id equals d.RefId
                                  join x in queryGroupMax
                                         on new { A = d.RefId, B = d.CreateDate } equals new {A = x.Key, B = x.LastDate}
                             //where e.ProductId != null
                             orderby d.CreateDate descending
                             select new ResponseApplicationModel
                             {
                                 ApplicationNo = e.ApplicationNo,
                                 FlagRead = e.FlagReadAgent,
                                 Message = d.Message,
                                 LastDate = d.CreateDate.Date == DateTime.Now.Date? d.CreateDate.Hour.ToString()+":"+ d.CreateDate.Minute.ToString() 
                                                                                : d.CreateDate.Year == DateTime.Now.Year? d.CreateDate.Day.ToString() + "/" + d.CreateDate.Month.ToString() : d.CreateDate.Day.ToString() + "/" + d.CreateDate.Month.ToString()+ "/" + d.CreateDate.Year.ToString()
                             };

                result = resultGroup.Select(s => new ResponseApplicationModel { ApplicationNo = s.ApplicationNo, FlagRead = s.FlagRead, Message = s.Message, LastDate = s.LastDate }).ToList();

            }
            else
            {
                var dataHead = _dbContext.ChatAgnPolicy;
                var dataDetail = _dbContext.ChatAgnPolicyDetail;

                var queryGroupMax =
                    from Detail in dataDetail
                    group Detail by Detail.RefId into dataDetailGroup
                    select new
                    {
                        Key = dataDetailGroup.Key,
                        LastDate = (
                            from Detail2 in dataDetailGroup
                            select Detail2.CreateDate
                        ).Max()
                    };


                var resultGroup = from e in dataHead
                                  join d in dataDetail
                                         on e.id equals d.RefId
                                  join x in queryGroupMax
                                         on new { A = d.RefId, B = d.CreateDate } equals new { A = x.Key, B = x.LastDate }
                                  where e.CreateBy == request.UserId.ToString()
                                  orderby d.CreateDate descending
                                  select new ResponseApplicationModel
                                  {
                                      ApplicationNo = e.ApplicationNo,
                                      FlagRead = e.FlagReadAdmin,
                                      Message = d.Message,
                                      LastDate = d.CreateDate.Date == DateTime.Now.Date ? d.CreateDate.Hour.ToString() + ":" + d.CreateDate.Minute.ToString()
                                                                                     : d.CreateDate.Year == DateTime.Now.Year ? d.CreateDate.Day.ToString() + "/" + d.CreateDate.Month.ToString() : d.CreateDate.Day.ToString() + "/" + d.CreateDate.Month.ToString() + "/" + d.CreateDate.Year.ToString()
                                  };

                result = resultGroup.Select(s => new ResponseApplicationModel { ApplicationNo = s.ApplicationNo, FlagRead = s.FlagRead, Message = s.Message, LastDate = s.LastDate }).ToList();
            }

            return result;
        }

        public async Task<List<ResponseHistoryModel>> GetHistory(RequestHistory request)
        {
            List<ResponseHistoryModel> result = new List<ResponseHistoryModel>();

            var dataDetail = _dbContext.ChatAgnPolicyDetail;

            var resultGroup = from d in dataDetail
                              where d.ApplicationNo == request.ApplicationNo
                              orderby d.CreateDate ascending
                              select new ResponseHistoryModel
                              {
                                  UserId = d.UserId,
                                  UserFullName = d.UserFullName,
                                  Message = d.Message
                              };

            result = resultGroup.Select(s => new ResponseHistoryModel { UserId = s.UserId, UserFullName = s.UserFullName, Message = s.Message}).ToList();
            return result;
        }

        public async Task<int> InsertChatDetail(RequestChatDetail request)
        {
            //var sequence = GetSequenceLogDeatilAsync(Id) + 1;
            var idByApplicatioNo = _dbContext.ChatAgnPolicy.Where(u => u.ApplicationNo == request.ApplicatioNo).FirstOrDefault().id;

            _dbContext.ChatAgnPolicyDetail.Add(new ChatAgnPolicyDetail()
            {
                Id = new Guid(),
                ApplicationNo = request.ApplicatioNo,
                UserId = request.UserId,
                UserFullName = request.UserFullName,
                Message = request.Message,
                CreateDate = DateTime.Now,
                RefId = idByApplicatioNo
            });
            return _dbContext.SaveChanges();
        }

        public async Task<int> UpdateChatHearAdmin(RequestFlageRead request)
        {
            //var sequence = GetSequenceLogDeatilAsync(Id) + 1;
            var update = _dbContext.ChatAgnPolicy.Where(u => u.ApplicationNo == request.ApplicatioNo).FirstOrDefault();
            update.FlagReadAdmin = request.Flag;

            return _dbContext.SaveChanges();
        }

        public async Task<int> UpdateChatHearAgent(RequestFlageRead request)
        {
            //var sequence = GetSequenceLogDeatilAsync(Id) + 1;
            //var idByApplicatioNo = _dbContext.ChatAgnPolicy.Where(u => u.ApplicationNo == request.ApplicatioNo).FirstOrDefault().id;

            var update = _dbContext.ChatAgnPolicy.Where(u => u.ApplicationNo == request.ApplicatioNo).FirstOrDefault();
            update.FlagReadAgent = request.Flag;

            return _dbContext.SaveChanges();           
        }
    }
}
