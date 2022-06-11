using System.Threading.Tasks;
using tickets.Data.Base;
using tickets.Data.ViewModel;
using tickets.Models;

namespace tickets.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropDownsVM> GetNewMovieDropDownsValues();

        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
}
