namespace MovieWebApi.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ImdbRate { get; set; }
        public int GenreId { get; set; }
        public GenreModel Genre { get; set; }
    }
}
