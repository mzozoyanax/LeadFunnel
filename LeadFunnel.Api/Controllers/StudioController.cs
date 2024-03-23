using LeadFunnel.Domain.Models;
using LeadFunnel.Domain;
using LeadFunnel.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LeadFunnel.Domain.ViewModels;

namespace LeadFunnel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly ITwilioService _service;

        public StudioController(ITwilioService service) 
        {
            _service = service; 
        }

        [HttpPost]
        [Route("TriggerFlow")]
        public async Task<bool> TriggerFlow(RegisterViewModel registerViewModel, string flowSID) 
        {
            return await _service.TriggerStudioFlow(registerViewModel, flowSID);
        }
    }
}
