using LeadFunnel.Domain;
using LeadFunnel.Domain.Models;
using LeadFunnel.Domain.ViewModels;
using LeadFunnel.Interface.Services;
using LeadFunnel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadFunnel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IEntityService<Contacts> _contact;
        private readonly IEntityService<Survey> _survey;

        public RegisterController(IEntityService<Contacts> contact, IEntityService<Survey> survey, AppDbContext appDbContext)
        {
            _contact = contact;
            _survey = survey;
        }

        [HttpPost]
        [Route("RegisterLead")]
        public bool Post(RegisterViewModel registerViewModel)
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
                    HowDidYouHearAboutUs = registerViewModel.HowDidYouHearAboutUs
                };

                _contact.Add(contacts);
                _survey.Add(survey);
            }
            catch (Exception ex) 
            {
                return false;
            }

            return true;
        }
    }
}
