namespace MovieWeb.WebApi.Helper
{    
    using System.Threading.Tasks;

    public interface ILocalFileStorage
    {
        Task DeleteFile(string path, string folder);
        
        Task<string> SaveFile(byte[] file, string extension, string folder);

        Task<string> EditFile(byte[] file, string extension, string folder, string pathActualFile);
    }
}
