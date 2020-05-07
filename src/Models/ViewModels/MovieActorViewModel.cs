namespace MovieWeb.WebApi.Model
{
    public class MovieActorViewModel
    {
        public int PersonId { get; set; }

        public int MovieId { get; set; }
        
        public string Character { get; set; }
        
        public int Order { get; set; }
    }
}
