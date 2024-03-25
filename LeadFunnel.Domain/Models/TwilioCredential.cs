using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Domain.Models
{
    public class TwilioCredential : BaseEntity
    {
        public string? AccountId { get; set; }  

        public string? ApiSecret { get; set; }

        public string? VirtualPhone { get; set; }

        public string? PersonalPhone { get; set; }  
    }
}
