using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using tickets.Data;
using tickets.Data.Services;
using tickets.Models;

namespace tickets.Controllers
{
    public class MoviesController : Controller
    {

        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Cinema);
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _service.GetAllAsync(n => n.Cinema);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filtered_result = data.Where(m => m.Name.ToLower().Contains(searchString.ToLower()) || m.Description.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filtered_result);
            }

            return View("Index", data);
        }

        // GET: movies/details/id

        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetMovieByIdAsync(id);
            return View(data);
        }

        // GET: movies/create
        public async Task<IActionResult> Create()
        {
            var dropdown_data = await _service.GetNewMovieDropDownsValues();
            
            ViewBag.CinemaList = new SelectList(dropdown_data.Cinemas, "Id", "Name");
            ViewBag.ProducerList = new SelectList(dropdown_data.Producers, "Id", "FullName");
            ViewBag.ActorList = new SelectList(dropdown_data.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var dropdown_data = await _service.GetNewMovieDropDownsValues();

                ViewBag.CinemaList = new SelectList(dropdown_data.Cinemas, "Id", "Name");
                ViewBag.ProducerList = new SelectList(dropdown_data.Producers, "Id", "FullName");
                ViewBag.ActorList = new SelectList(dropdown_data.Actors, "Id", "FullName");
                
                return View(movie);
            }

            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }



        // GET: movies/edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetMovieByIdAsync(id);
            if (data == null)
                return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                CinemaId = data.CinemaId,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId,
                ActorIds = data.Actors_Movies.Select(n => n.ActorId).ToList()
            };

            var dropdown_data = await _service.GetNewMovieDropDownsValues();

            ViewBag.CinemaList = new SelectList(dropdown_data.Cinemas, "Id", "Name");
            ViewBag.ProducerList = new SelectList(dropdown_data.Producers, "Id", "FullName");
            ViewBag.ActorList = new SelectList(dropdown_data.Actors, "Id", "FullName");

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie_data)
        {
            if(id != movie_data.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                var dropdown_data = await _service.GetNewMovieDropDownsValues();

                ViewBag.CinemaList = new SelectList(dropdown_data.Cinemas, "Id", "Name");
                ViewBag.ProducerList = new SelectList(dropdown_data.Producers, "Id", "FullName");
                ViewBag.ActorList = new SelectList(dropdown_data.Actors, "Id", "FullName");

                return View(movie_data);
            }

            await _service.UpdateMovieAsync(movie_data);
            return RedirectToAction(nameof(Index));

        }

    }
}
