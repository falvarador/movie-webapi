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

    public class PersonService : IPersonService
    {
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            _context.People.Remove(_mapper.Map<Person>(entity));
            return await _context.SaveChangesAsync().ToBooleanAsync();
        }

        public async Task<List<PersonViewModel>> GetAllAsync()
        {
           var entities = await _context.People.ToListAsync();
           return _mapper.Map<List<PersonViewModel>>(entities);           
        }

        public async Task<PersonViewModel> GetAsync(int id)
        {
            var entity = await _context.People.FindAsync(id);
            return _mapper.Map<PersonViewModel>(entity);
        }
    
        public async Task<(bool isSuccess, int id )> InsertAsync(PersonViewModel entity)
        {
            var value = await _context.People.AddAsync(_mapper.Map<Person>(entity));
            var result = await _context.SaveChangesAsync().ToBooleanAsync();

            return (result, entity.Id);
        }

        public async Task<bool> UpdateAsync(PersonViewModel entity)
        {
            _context.People.Update(_mapper.Map<Person>(entity));
            return await _context.SaveChangesAsync().ToBooleanAsync();
        }

        private readonly IMapper _mapper;
        private readonly InfraestructureContext _context;

        public PersonService() { }

        public PersonService(IMapper mapper, InfraestructureContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
