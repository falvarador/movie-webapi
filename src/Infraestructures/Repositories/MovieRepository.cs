namespace MovieWeb.WebApi.Infraestructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MovieWeb.WebApi.Model;

    public class MovieRepository : IMovieRepository
    {
        public MovieRepository()
        {
            // _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return true;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return new List<Movie>() 
            {
                new Movie()
            };
        }

        public async Task<Movie> GetAsync(int id)
        {
            return new Movie();
        }

        public async Task<bool> InsertAsync(Movie entity)
        {
           return true;
        }

        public async Task<bool> UpdateAsync(Movie entity)
        {
            return true;
        }
    }
}
