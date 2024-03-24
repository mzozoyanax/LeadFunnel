using LeadFunnel.Domain.Models;
using LeadFunnel.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadFunnel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredApiController : ControllerBase
    {
        private readonly IEntityService<TwilioCredential> _service;

        public CredApiController(IEntityService<TwilioCredential> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<TwilioCredential> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("Get")]

        public TwilioCredential Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("Add")]

        public void Add(TwilioCredential twilioCredential)
        {
            _service.Add(twilioCredential);
        }

        [HttpPut]
        [Route("Update")]

        public void Update(TwilioCredential twilioCredential)
        {
            _service.Update(twilioCredential);
        }

        [HttpDelete]
        [Route("Delete")]

        public void Delete(int id)
        {
            _service.Delete(_service.Get(id));
        }

        [HttpDelete]
        [Route("DeleteAll")]

        public void DeleteAll()
        {
            _service.RemoveAll(_service.GetAll());
        }
    }
}
