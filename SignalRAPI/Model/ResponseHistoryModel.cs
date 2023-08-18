using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Model
{
    public class ResponseHistoryModel
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string Message { get; set; }
    }

    public class ResponseHistoryListModel
    {
        public List<ResponseHistoryModel> ResponseHistoryModel { get; set; }
    }
}
