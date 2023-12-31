﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAPI.Entities
{
    [Table("chat_agn_policy_Detail")]
    public class ChatAgnPolicyDetail
    {
        public Guid Id { get; set; }
        public string ApplicationNo { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid RefId { get; set; }
    }
}
