using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using tickets.Data;
using tickets.Data.Services;
using tickets.Models;

namespace tickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // Get: Actors/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Biography")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor); // returns the actor model but with the validation errors. 
            }

            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null)
                return View("NotFound");

            return View(actor);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return View("NotFound");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Biography")]Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);

            if(id == actor.Id)
			{
                await _service.UpdateAsync(id, actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return View("NotFound");
            return View(data);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
