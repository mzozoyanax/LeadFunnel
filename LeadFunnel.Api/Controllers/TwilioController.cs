using LeadFunnel.Domain;
using LeadFunnel.Domain.Models;
using LeadFunnel.Domain.ViewModels;
using LeadFunnel.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadFunnel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioController : ControllerBase
    {
        private readonly IEntityService<Contacts> _contacts;
        private readonly ITwilioService _twilioService;

        public TwilioController(IEntityService<Contacts> contacts, ITwilioService twilioService)
        {
            _contacts = contacts;
            _twilioService = twilioService;
        }

        [HttpPost]
        [Route("RegisterContact")] //First time landing page sign up users will be saved in the database...
        public bool Register(RegisterViewModel registerViewModel)
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
        [Route("SendText")]
        public bool SendTextMessage(MessageViewModel messageViewModel)
        {
            return _twilioService.SendTextToIndividualContact(messageViewModel);
        }

        [HttpPost]
        [Route("TriggerFlow")] //The user will get notification on their phone when done signing up....
        public async Task<bool> TriggerFlow(RegisterViewModel registerViewModel, string flowSid)
        {
            //This method will trigger the Studio Flow that I created in Twilio...
            return await _twilioService.TriggerStudioFlow(registerViewModel, flowSid);
        }


        [HttpPost]
        [Route("NotifyYourself")] //You will get notification on their phone when done signing up....
        public bool NotifyYourself()
        {
            return _twilioService.ForwardTextMessages();
        }

        [HttpGet]
        [Route("GetTextMessages")]
        public List<MessageViewModel> GetTexts()
        {
            //Get all text messages from Twilio....
            return _twilioService.TwilioTextMessages();
        }
    }
}
