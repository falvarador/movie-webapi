namespace MovieWeb.WebApi.Helper
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class LocalFileStorage : ILocalFileStorage
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalFileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Task DeleteFile(string path, string folder)
        {
            var filename = Path.GetFileName(path);
            var dirFile = Path.Combine(_env.WebRootPath, folder, filename);
            
            if (File.Exists(dirFile))
            {
                File.Delete(dirFile);
            }

            return Task.FromResult(0);
        }

        public async Task<string> SaveFile(byte[] file, string extension, string folder)
        {
            var filename = $"{Guid.NewGuid()}.{extension}";
            string dirFile = Path.Combine(_env.WebRootPath, folder);

            if (!Directory.Exists(dirFile))
            {
                Directory.CreateDirectory(dirFile);
            }

            string dirSaveFile = Path.Combine(dirFile, filename);
            await File.WriteAllBytesAsync(dirSaveFile, file);

            var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var pathToDB = Path.Combine(url, dirFile, filename);
            
            return pathToDB;
        }

        public async Task<string> EditFile(byte[] file, string extension, string folder, string pathActualFile)
        {
            if (!string.IsNullOrEmpty(pathActualFile))
            {
                await DeleteFile(pathActualFile, folder);
            }

            return await SaveFile(file, extension, folder);
        }
    }
}
