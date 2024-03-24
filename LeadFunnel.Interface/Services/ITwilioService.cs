using LeadFunnel.Domain.Models;
using LeadFunnel.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Interface.Services
{
    public interface ITwilioService
    {
        Task<bool> TriggerStudioFlow(RegisterViewModel registerViewModel, string FlowSid);

        List<MessageViewModel> TwilioTextMessages(string virtualPhoneNumber);
    }
}
