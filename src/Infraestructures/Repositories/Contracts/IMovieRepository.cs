namespace MovieWeb.WebApi.Infraestructure
{
    using MovieWeb.WebApi.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieRepository
    {
        Task<bool> DeleteAsync(int id);

        Task<List<Movie>> GetAllAsync();

        Task<Movie> GetAsync(int id);

        Task<bool> InsertAsync(Movie entity);

        Task<bool> UpdateAsync(Movie entity);
    }
}
