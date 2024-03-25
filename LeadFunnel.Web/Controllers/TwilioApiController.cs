using LeadFunnel.Domain.Models;
using LeadFunnel.Domain.ViewModels;
using LeadFunnel.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace LeadFunnel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioApiController : ControllerBase
    {
        private readonly IEntityService<Contacts> _contacts;
        private readonly IEntityService<TwilioCredential> _credential;
        private readonly ITwilioService _twilioService;

        public TwilioApiController(IEntityService<Contacts> contacts, 
            IEntityService<TwilioCredential> credential, 
            ITwilioService twilioService
            )
        {
            _contacts = contacts;
            _twilioService = twilioService;
            _credential = credential;
        }

        [HttpGet]
        [Route("GetTextMessagesFromLeads")]
        public List<MessageViewModel> GetTextMessages()
        {
            //Get all text messages from Twilio....
            return _twilioService.TwilioTextMessages();
        }

        [HttpPost]
        [Route("RegisterLead")] 
        public bool RegisterLead(RegisterViewModel registerViewModel)
        {
            try
            {
                Contacts contacts = new Contacts()
                {
                    Name = registerViewModel.Name,
                    Email = registerViewModel.Email,
                    Phone = registerViewModel.Phone,
                    Company = registerViewModel.Company,
                };

                _contacts.Add(contacts);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        [Route("ForwardTextMessage")] 
        public bool ForwardTextMessage()
        {
            return _twilioService.ForwardTextMessages();
        }

        [HttpPost]
        [Route("SendGroupTextMessages")]
        public bool SendGroupTextMessages(MessageViewModel messageViewModel)
        {
            return _twilioService.SendTextToAllContact(messageViewModel);
        }

        [HttpPost]
        [Route("SendIndividualTextMessages")]
        public bool SendIndividualTextMessages(MessageViewModel messageViewModel)
        {
            return _twilioService.SendTextToIndividualContact(messageViewModel);
        }

        [HttpPost]
        [Route("ActivateWorkflow")]
        public bool ActivateWorkflow(int GroupId)
        {
            return _twilioService.RunWorkflow(GroupId);
        }
    }
}
