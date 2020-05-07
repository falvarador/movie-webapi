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
        
        public ICollection<MovieActorViewModel> Actors { get; set; }

        public ICollection<GenderViewModel> Genders { get; set; }
    }
}
