using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Domain.Models
{
    public class TwilioWorkflow : BaseEntity
    {
        public int GroupId { get; set; }    

        public bool Reply { get; set; }

        public string? InitialMessage { get; set; }

        public string? FollowupMessage { get; set; }

        public int Delay { get; set; }

    }
}
