namespace MovieWebApi.Models
{
    public class GenreModel
    {
        public int Id  { get; set; }
        public string Title { get; set; }
        public ICollection<MovieModel> Movies { get; set; }
    }
}
