namespace MovieWeb.WebApi.Infraestructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MovieWeb.WebApi.Model;

    public class MovieGenderMapping : IEntityTypeConfiguration<MovieGender>
    {
        public void Configure(EntityTypeBuilder<MovieGender> builder)
        {
            builder.HasKey(k => new { k.GenderId, k.MovieId });
        }
    }
}
