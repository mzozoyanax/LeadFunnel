using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Domain.Models
{
    public class TwilioStudioFlows : BaseEntity
    {
        public string? FlowSID { get; set; } 

        public string? FlowName { get; set; }
    }
}
