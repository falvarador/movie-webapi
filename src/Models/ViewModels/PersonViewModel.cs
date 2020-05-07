namespace MovieWeb.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PersonViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Biography { get; set; }
        
        public string Photo { get; set; }
        
        [Required]
        public DateTime? Birthdate { get; set; }
    }

    public class MoviesPerPersonViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
                
        public ICollection<MovieViewModel> Movies { get; set; }
    }
}
