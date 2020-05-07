namespace MovieWeb.WebApi.Service
{
    using AutoMapper;
    using MovieWeb.WebApi.Infraestructure;
    using MovieWeb.WebApi.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MovieService : IMovieService
    {
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<MovieViewModel>> GetAllAsync()
        {
           var entities = await _repository.GetAllAsync();
           return _mapper.Map<List<MovieViewModel>>(entities);           
        }

        public async Task<MovieViewModel> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            return _mapper.Map<MovieViewModel>(entity);
        }
    
        public async Task<(bool isSuccess, int id )> InsertAsync(MovieViewModel entity)
        {
            var result = await _repository.InsertAsync(_mapper.Map<Movie>(entity));
            return (result, entity.Id);
        }

        public async Task<bool> UpdateAsync(MovieViewModel entity)
        {
            return await _repository.UpdateAsync(_mapper.Map<Movie>(entity));
        }

        private readonly IMapper _mapper;
        private readonly IMovieRepository _repository;

        public MovieService() { }

        public MovieService(IMapper mapper, IMovieRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
