using LeadFunnel.Domain.Models;
using LeadFunnel.Domain;
using LeadFunnel.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LeadFunnel.Services;

namespace LeadFunnel.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private readonly IEntityService<TwilioCredential> _service;

        public CredentialsController(IEntityService<TwilioCredential> service)
        {
            _service = service;
        }

        [HttpGet]
        public List<TwilioCredential> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet]
        public TwilioCredential Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public void Add(TwilioCredential account)
        {
            _service.Add(account);
        }

        [HttpPut]
        public void Update(TwilioCredential account)
        {
            _service.Update(account);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _service.Delete(Get(id));
        }

        [HttpDelete]
        public void DeleteAll()
        {
            _service.RemoveAll(_service.GetAll());
        }
    }
}
