namespace MovieWeb.WebApi.Service
{
    using MovieWeb.WebApi.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieService
    {
        Task<bool> DeleteAsync(int id);

        Task<List<MovieViewModel>> GetAllAsync();

        Task<MovieViewModel> GetAsync(int id);

       Task<(bool isSuccess, int id )> InsertAsync(MovieViewModel entity);

        Task<bool> UpdateAsync(MovieViewModel entity);
    }
}
