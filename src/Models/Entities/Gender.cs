namespace MovieWeb.WebApi.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Gender
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

        public ICollection<MovieGender> Movies { get; set; }
    }
}
