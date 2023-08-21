using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Model
{
    public class RequestFlageRead
    {
        public string ApplicatioNo { get; set; }
        public string UserType { get; set; }
        public bool Flag { get; set; }
    }
}
