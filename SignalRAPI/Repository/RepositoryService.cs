using Microsoft.EntityFrameworkCore;
using SignalRAPI.Context;
using SignalRAPI.Entities;
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

        public async Task<List<NotiPolicy>> GetNotiPolicyAsync(string ApplicationNo)
        {
            var result = _dbContext.NotiPolicy.ToList();

            return result;
        }

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
    }
}
