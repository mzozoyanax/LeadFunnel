using LeadFunnel.Domain;
using LeadFunnel.Domain.Models;
using LeadFunnel.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadFunnel.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredController : ControllerBase
    {
        private readonly IEntityService<TwilioCredential> _entityService;
        private readonly AppDbContext _appDbContext;

        public CredController(IEntityService<TwilioCredential> entityService, AppDbContext appDbContext)
        {
            _entityService = entityService;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public List<TwilioCredential> GetAll()
        {
            return _entityService.GetAll();
        }

        [HttpGet]
        public TwilioCredential Get(int id)
        {
            return _entityService.Get(id);
        }

        [HttpPost]
        public void Add(TwilioCredential account)
        {
            _entityService.Add(account);
        }

        [HttpPut]
        public void Update(TwilioCredential account)
        {
            _entityService.Update(account);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _entityService.Delete(Get(id));
        }

        [HttpDelete]
        public void DeleteAll()
        {
            _entityService.RemoveAll(_entityService.GetAll());
        }
    }
}
