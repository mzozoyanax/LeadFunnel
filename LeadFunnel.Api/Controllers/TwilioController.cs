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
        private readonly IEntityService<Survey> _survey;
        private readonly ITwilioService _twilioService;

        public TwilioController(IEntityService<Contacts> contacts, IEntityService<Survey> survey, ITwilioService twilioService)
        {
            _contacts = contacts;
            _survey = survey;
            _twilioService = twilioService;
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

                Survey survey = new Survey()
                {
                    ContactPreference = registerViewModel.ContactPreference,
                    ProjectInformation = registerViewModel.ProjectInformation,
                    HowDidYouHearAboutUs = registerViewModel.HowDidYouHearAboutUs,
                };

                _contacts.Add(contacts);
                _survey.Add(survey);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        [Route("NotifyLead")]
        public async Task<bool> NotifyLead(RegisterViewModel registerViewModel, string flowSid)
        {
            return await _twilioService.TriggerStudioFlow(registerViewModel, flowSid);
        }
    }
}
