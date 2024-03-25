using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Domain.Models
{
    public class TwilioWorkflowGroups : BaseEntity
    {
        public int GroupId { get; set; }

        public string? Name { get; set; }

        public bool Running { get; set; }
    }
}
