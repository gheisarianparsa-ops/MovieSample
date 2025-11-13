namespace MovieWebApi.Models
{
    public class GetAllMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal ImdbRate { get; set; }
        public string GenreTitle { get; set; }
    }
}
