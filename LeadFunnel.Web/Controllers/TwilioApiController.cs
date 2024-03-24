﻿using LeadFunnel.Domain.Models;
using LeadFunnel.Domain.ViewModels;
using LeadFunnel.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadFunnel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioApiController : ControllerBase
    {
        private readonly IEntityService<Contacts> _contacts;
        private readonly IEntityService<Survey> _survey;
        private readonly ITwilioService _twilioService;

        public TwilioApiController(IEntityService<Contacts> contacts, IEntityService<Survey> survey, ITwilioService twilioService)
        {
            _contacts = contacts;
            _survey = survey;
            _twilioService = twilioService;
        }

        [HttpPost]
        [Route("RegisterContact")]
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

                Survey survey = new Survey()
                {
                    ContactPreference = registerViewModel.ContactPreference,
                    ProjectInformation = registerViewModel.ProjectInformation,
                    HowDidYouHearAboutUs = registerViewModel.HowDidYouHearAboutUs,
                };

                _contacts.Add(contacts);
                _survey.Add(survey);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        [Route("NotifyContact")]
        public async Task<bool> Notify(RegisterViewModel registerViewModel, string flowSid)
        {
            return await _twilioService.TriggerStudioFlow(registerViewModel, flowSid);
        }

        [HttpGet]
        [Route("GetTextMessages")]
        public List<MessageViewModel> GetTexts(string virtualPhoneNumber)
        {
            return _twilioService.TwilioTextMessages(virtualPhoneNumber);
        }
    }
}