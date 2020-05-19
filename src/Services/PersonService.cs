namespace MovieWeb.WebApi.Service
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MovieWeb.WebApi.Common;
    using MovieWeb.WebApi.Helper;
    using MovieWeb.WebApi.Infraestructure;
    using MovieWeb.WebApi.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PersonService : IPersonService
    {
        public async Task<bool> DeleteAsync(int id)
        {
            var exist = await _context.People.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return false;
            }

            _context.Remove(new Person { Id = id });
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
            if (!string.IsNullOrWhiteSpace(entity.Photo))
            {
                var photo = Convert.FromBase64String(entity.Photo);
                entity.Photo = await _localFileStorage.SaveFile(photo, "jpg", nameof(Person));
            }

            var value = await _context.People.AddAsync(_mapper.Map<Person>(entity));
            var result = await _context.SaveChangesAsync().ToBooleanAsync();

            return (result, entity.Id);
        }

        public async Task<bool> UpdateAsync(PersonViewModel entity)
        {
            var _entity = await _context.People.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (_entity == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(entity.Photo))
            {
                var photo = Convert.FromBase64String(entity.Photo);
                _entity.Photo = await _localFileStorage.EditFile(photo, "jpg", nameof(Person), _entity.Photo);
            }
            
            _context.People.Update(_mapper.Map<Person>(entity));
            return await _context.SaveChangesAsync().ToBooleanAsync();
        }

        private readonly IMapper _mapper;
        private readonly InfraestructureContext _context;
        private readonly ILocalFileStorage _localFileStorage;

        public PersonService() { }

        public PersonService(IMapper mapper, InfraestructureContext context, ILocalFileStorage localFileStorage)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _localFileStorage = localFileStorage ?? throw new ArgumentNullException(nameof(localFileStorage));
        }
    }
}
