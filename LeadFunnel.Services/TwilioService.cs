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
        public Task<bool> TriggerStudioFlow(RegisterViewModel registerViewModel, string FlowSid)
        {
            return _repository.TriggerStudioFlow(registerViewModel, FlowSid);
        }

        public List<MessageViewModel> TwilioTextMessages(string virtualPhoneNumber)
        {
            return _repository.TwilioTextMessages(virtualPhoneNumber);  
        }
    }
}
