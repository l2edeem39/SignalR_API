using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Entities
{
    [Table("chat_agn_policy")]
    public class ChatAgnPolicy
    {
        public Guid id { get; set; }
        public string ApplicationNo { get; set; }
        public string IssueDetail { get; set; }
        public bool? FlagReadAgent { get; set; }
        public bool? FlagReadAdmin { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
