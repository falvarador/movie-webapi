namespace MovieWeb.WebApi.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InsertGenderViewModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class GenderViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class MoviesPerGenderViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<MovieViewModel> Movies { get; set; }
    }
}
