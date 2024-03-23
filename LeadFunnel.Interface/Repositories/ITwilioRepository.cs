using LeadFunnel.Domain.Models;
using LeadFunnel.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Interface.Repositories
{
    public interface ITwilioRepository
    {
        Task<bool> TriggerStudioFlow(RegisterViewModel registerViewModel, string FlowSid);
    }
}
