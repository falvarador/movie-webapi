namespace MovieWeb.WebApi.Infraestructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MovieWeb.WebApi.Common;
    using MovieWeb.WebApi.Model;

    public class MovieRepository : IMovieRepository
    {
        private readonly InfraestructureContext _context;

        public MovieRepository(InfraestructureContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exist = await _context.People.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return false;
            }

            _context.Remove(new Movie { Id = id });
            return await _context.SaveChangesAsync().ToBooleanAsync();
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<bool> InsertAsync(Movie entity)
        {
            await _context.Movies.AddAsync(entity);
            return await _context.SaveChangesAsync().ToBooleanAsync();
        }

        public async Task<bool> UpdateAsync(Movie entity)
        {
            _context.Movies.Update(entity);
            return await _context.SaveChangesAsync().ToBooleanAsync();
        }
    }
}
