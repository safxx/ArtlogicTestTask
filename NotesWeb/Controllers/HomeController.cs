using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NotesWeb.ViewModels;

namespace NotesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var settingsViewModel = new SettingsViewModel
            {
                NotesApiAddress = _configuration.GetSection("Api").GetSection("NotesApi").Value
            };
            return View(settingsViewModel);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
