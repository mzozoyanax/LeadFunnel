using LeadFunnel.Interface.Services;
using LeadFunnel.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeadFunnel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITwilioService _twilioService;

        public HomeController(ILogger<HomeController> logger, ITwilioService twilioService)
        {
            _logger = logger;
            _twilioService = twilioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
