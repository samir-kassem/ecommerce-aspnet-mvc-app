using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using tickets.Data.Base;
using tickets.Data.ViewModel;
using tickets.Models;

namespace tickets.Data.Services
{
    public class MoviesService:EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _db;
        public MoviesService(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var new_movie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                CinemaId = data.CinemaId,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await _db.Movies.AddAsync(new_movie);
            await _db.SaveChangesAsync();

            foreach(var actor_id in data.ActorIds)
            {
                var new_actor_movie = new Actor_Movie()
                {
                    ActorId = actor_id,
                    MovieId = new_movie.Id
                };
                await _db.Actors_Movies.AddAsync(new_actor_movie);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var data = await _db.Movies
                .Include(n => n.Cinema)
                .Include(n => n.Producer)
                .Include(n => n.Actors_Movies).ThenInclude(n => n.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);
            return data;
        }

        public async Task<NewMovieDropDownsVM> GetNewMovieDropDownsValues()
        {
            var data = new NewMovieDropDownsVM();
            data.Actors = await _db.Actors.OrderBy(n => n.FullName).ToListAsync();
            data.Cinemas = await _db.Cinemas.OrderBy(n => n.Name).ToListAsync();
            data.Producers = await _db.Producers.OrderBy(n => n.FullName).ToListAsync();
            return data;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var db_movie = await _db.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(db_movie != null)
            {
                db_movie.Name = data.Name;
                db_movie.Description = data.Description;
                db_movie.Price = data.Price;
                db_movie.ImageURL = data.ImageURL;
                db_movie.StartDate = data.StartDate;
                db_movie.EndDate = data.EndDate;
                db_movie.CinemaId = data.CinemaId;
                db_movie.MovieCategory = data.MovieCategory;
                db_movie.ProducerId = data.ProducerId;

                //_db.Movies.Update(db_movie);
                await _db.SaveChangesAsync();
            }

            var existing_actors_db = _db.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _db.Actors_Movies.RemoveRange(existing_actors_db);
            await _db.SaveChangesAsync();
            

            foreach (var actor_id in data.ActorIds)
            {
                var new_actor_movie = new Actor_Movie()
                {
                    ActorId = actor_id,
                    MovieId = data.Id
                };
                await _db.Actors_Movies.AddAsync(new_actor_movie);
            }
            await _db.SaveChangesAsync();
        }
    }
}
