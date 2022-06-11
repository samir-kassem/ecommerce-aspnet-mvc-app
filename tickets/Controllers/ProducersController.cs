using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using tickets.Data;
using tickets.Data.Services;
using tickets.Models;

namespace tickets.Controllers
{
    public class ProducersController : Controller
    {

        private IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // GET: producers/details/id
        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return View("NotFound");

            return View(data);
        }

        // GET: producers/create
        public IActionResult Create()
		{
            return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Biography")]Producer producer)
		{
			if (!ModelState.IsValid)
			{
                return View(producer);
			}

            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        // GET: producers/edit/id
        public async Task<IActionResult> Edit(int id)
		{
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return View("NotFound");
            return View(data);
		}

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Biography")]Producer producer)
		{
            if (!ModelState.IsValid)
                return View(producer);

            if(id == producer.Id)
			{
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
           
		}


        // GET: producers/delete/id
        public async Task<IActionResult> Delete(int id)
		{
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return View("NotFound");
            return View(data);
		}

        [HttpPost, ActionName("Delete")]
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
