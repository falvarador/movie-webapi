namespace MovieWeb.WebApi.Service
{
    using MovieWeb.WebApi.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenderService
    {
        Task<bool> DeleteAsync(int id);

        Task<List<GenderViewModel>> GetAllAsync();

        Task<GenderViewModel> GetAsync(int id);

        Task<(bool isSuccess, int id )> InsertAsync(InsertGenderViewModel entity);

        Task<bool> UpdateAsync(GenderViewModel entity);
    }
}
