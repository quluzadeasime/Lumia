using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAppTest.Controllers
{
    
    public class HomeController : Controller
    {
        ITeamService _teamService;

        public HomeController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            var teams = _teamService.GetAll();
            return View(teams);
        }
    }
}
