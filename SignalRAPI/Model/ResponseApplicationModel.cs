using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Model
{
    public class ResponseApplicationModel
    {
        public string ApplicationNo { get; set; }
        public bool? FlagRead { get; set; }
        public string Message { get; set; }
        public string LastDate { get; set; }
    }

    public class ResponseApplicationListModel
    {
        public List<ResponseApplicationModel> ResponseApplicationModel { get; set; }
    }
}
