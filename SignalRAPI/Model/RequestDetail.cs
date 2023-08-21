using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Model
{
    public class RequestDetail
    {
        public string ApplicatioNo { get; set; }
    }

    public class RequestChatDetail
    {
        public string ApplicatioNo { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string Message { get; set; }
        public string UserType { get; set; }
    }
}
