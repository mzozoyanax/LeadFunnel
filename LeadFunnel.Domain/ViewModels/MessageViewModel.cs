using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Domain.ViewModels
{
    public class MessageViewModel
    {
        public string? Sid { get; set; }

        public string? From {get; set; }

        public string? To { get; set; }

        public string? Body { get; set; }

        public DateTime? DateSent { get; set; }  
    }
}
