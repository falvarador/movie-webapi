namespace MovieWeb.WebApi.Service
{
    using MovieWeb.WebApi.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPersonService
    {
        Task<bool> DeleteAsync(int id);

        Task<List<PersonViewModel>> GetAllAsync();

        Task<PersonViewModel> GetAsync(int id);

        Task<(bool isSuccess, int id )> InsertAsync(PersonViewModel entity);

        Task<bool> UpdateAsync(PersonViewModel entity);
    }
}
