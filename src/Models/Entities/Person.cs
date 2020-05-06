namespace MovieWeb.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Biography { get; set; }
        
        public string Photo { get; set; }
        
        [Required]
        public DateTime? Birthdate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Person p2)
            {
                return Id == p2.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public ICollection<MovieActor> Movies { get; set; }
    }
}
