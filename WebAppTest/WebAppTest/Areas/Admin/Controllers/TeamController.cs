using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAppTest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            var teams = _teamService.GetAll();
            return View(teams);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _teamService.Add(team);
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View();  
            }
            catch(TeamNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(ContentTypeException ex) 
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if(!ModelState.IsValid) return View();

            try
            {
                _teamService.Delete(id);
            }
            catch(TeamNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();  
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var existTeam = _teamService.Get(x=>x.Id == id);
            if(existTeam == null) return View();
            return View(existTeam);
        }

        [HttpPost]
        public IActionResult Update(Team team)
        {
            if(!ModelState.IsValid) return View();

            try
            {
                _teamService.Update(team.Id, team);
            }
            catch (TeamNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return NotFound();
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
