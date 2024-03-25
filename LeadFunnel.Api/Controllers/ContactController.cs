using LeadFunnel.Domain.Models;
using LeadFunnel.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadFunnel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IEntityService<Contacts> _service;

        public ContactController(IEntityService<Contacts> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Contacts> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("Get")]

        public Contacts Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("Add")]

        public void Add(Contacts contacts)
        {
            _service.Add(contacts);
        }

        [HttpPut]
        [Route("Update")]

        public void Update(Contacts contacts)
        {
            _service.Update(contacts);
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
