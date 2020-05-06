namespace MovieWeb.WebApi.Infraestructure
{
    using Microsoft.EntityFrameworkCore;
    using MovieWeb.WebApi.Model;

    public class InfraestructureContext : DbContext 
    {
        public InfraestructureContext(DbContextOptions<InfraestructureContext> options)
            : base(options)
        {
            
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGender>().HasKey(x => new { x.GenderId, x.MovieId });
            modelBuilder.Entity<MovieActor>().HasKey(x => new { x.MovieId, x.PersonId });

            // var people = new List<Person>();

            // for (int i = 5; i < 100; i++)
            // {
            //    people.Add(new Person() { Id = i, Name = $"Person {i}", 
            //        Birthdate = DateTime.Today });
            // }

            // modelBuilder.Entity<Person>().HasData(people);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Movie> Movies { get; set; }
        
        public DbSet<Gender> Genders { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<MovieGender> MoviesGenders { get; set; }

        public DbSet<MovieActor> MoviesActors { get; set; }
    }
}
