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

        List<MessageViewModel> TwilioTextMessages();

        bool ForwardTextMessages();

        bool SendTextToAllContact(MessageViewModel messageViewModel);

        bool SendTextToIndividualContact(MessageViewModel messageViewModel);

        bool RunWorkflow(int Id);

        bool SendSimpleTextMessage(string Message);

        bool ActiveReply();

    }
}
