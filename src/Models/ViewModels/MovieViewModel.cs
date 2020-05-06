namespace MovieWeb.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Summary { get; set; }

        public bool IsInTheaters { get; set; }

        public string Trailer { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }
        
        public string Poster { get; set; }
                
        public string ShortTitle { get; set; }
        
        public ICollection<MovieActor> Actors { get; set; } = new HashSet<MovieActor>();

        public ICollection<MovieGender> Genders { get; set; } = new HashSet<MovieGender>();
    }
}
