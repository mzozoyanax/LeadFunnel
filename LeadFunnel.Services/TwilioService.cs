using LeadFunnel.Domain.ViewModels;
using LeadFunnel.Interface.Repositories;
using LeadFunnel.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly ITwilioRepository _repository;

        public TwilioService(ITwilioRepository repository) 
        {
            _repository = repository;
        }

        public bool ActiveReply()
        {
            return _repository.ActiveReply();
        }

        public bool ForwardTextMessages()
        {
            return _repository.ForwardTextMessages();   
        }

        public bool RunWorkflow(int Id)
        {
            return _repository.RunWorkflow(Id);
        }

        public bool SendSimpleTextMessage(string Message)
        {
            return _repository.SendSimpleTextMessage(Message);
        }

        public bool SendTextToAllContact(MessageViewModel messageViewModel)
        {
            return _repository.SendTextToAllContact(messageViewModel);
        }

        public bool SendTextToIndividualContact(MessageViewModel messageViewModel)
        {
            return _repository.SendTextToIndividualContact(messageViewModel);
        }

        public Task<bool> TriggerStudioFlow(RegisterViewModel registerViewModel, string FlowSid)
        {
            return _repository.TriggerStudioFlow(registerViewModel, FlowSid);
        }

        public List<MessageViewModel> TwilioTextMessages()
        {
            return _repository.TwilioTextMessages();  
        }
    }
}
