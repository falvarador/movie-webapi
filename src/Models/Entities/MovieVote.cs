namespace MovieWeb.WebApi.Model
{
    using System;

    public class MovieVote
    {
        public int Id { get; set; }
        
        public int Vote { get; set; }
        
        public DateTime VoteDate { get; set; }
        
        public int MovieId { get; set; }
        
        public Movie Movie { get; set; }
    }
}
