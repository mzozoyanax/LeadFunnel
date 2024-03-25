using Microsoft.AspNetCore.Mvc;

namespace LeadFunnel.Auth.Controllers
{
    public class TwilioAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
