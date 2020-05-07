namespace MovieWeb.WebApi.Profile
{
    using AutoMapper;
    using MovieWeb.WebApi.Model;

    public class GenderViewModelProfile : Profile
    {
        public GenderViewModelProfile()
        {
            CreateMap<GenderViewModel, Gender>().ReverseMap();
             CreateMap<InsertGenderViewModel, Gender>().ReverseMap();
        }
    }

    public class MovieViewModelProfile : Profile
    {
        public MovieViewModelProfile()
        {
            CreateMap<MovieViewModel, Movie>().ReverseMap();
        }
    }

    public class PersonViewModelProfile : Profile
    {
        public PersonViewModelProfile()
        {
            CreateMap<PersonViewModel, Person>().ReverseMap();
        }
    }
}
