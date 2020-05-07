namespace MovieWeb.WebApi.Service
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MovieWeb.WebApi.Common;
    using MovieWeb.WebApi.Infraestructure;
    using MovieWeb.WebApi.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GenderService : IGenderService
    {
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            _context.Genders.Remove(_mapper.Map<Gender>(entity));
            return await _context.SaveChangesAsync().ToBooleanAsync();
        }

        public async Task<List<GenderViewModel>> GetAllAsync()
        {
           var entities = await _context.Genders.ToListAsync();
           return _mapper.Map<List<GenderViewModel>>(entities);           
        }

        public async Task<GenderViewModel> GetAsync(int id)
        {
            var entity = await _context.Genders.FindAsync(id);
            return _mapper.Map<GenderViewModel>(entity);
        }
    
        public async Task<(bool isSuccess, int id )> InsertAsync(InsertGenderViewModel entity)
        {
            var value = await _context.Genders.AddAsync(_mapper.Map<Gender>(entity));
            var result = await _context.SaveChangesAsync().ToBooleanAsync();

            return (result, value.Entity.Id);
        }

        public async Task<bool> UpdateAsync(GenderViewModel entity)
        {
            _context.Genders.Update(_mapper.Map<Gender>(entity));
            return await _context.SaveChangesAsync().ToBooleanAsync();
        }

        private readonly IMapper _mapper;
        private readonly InfraestructureContext _context;

        public GenderService() { }

        public GenderService(IMapper mapper, InfraestructureContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
