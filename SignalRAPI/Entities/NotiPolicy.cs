using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Entities
{
    [Table("NotiPolicy")]
    public class NotiPolicy
    {
        public Guid id { get; set; }
        public string ApplicationNo { get; set; }
        public string IssueDetail { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
