using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Domain.Models
{
    public class Survey : BaseEntity
    {
        public string? ContactPreference { get; set; }  

        public string? ProjectInformation {get; set; }

        public string? HowDidYouHearAboutUs { get; set; }
    }
}
