using Microsoft.EntityFrameworkCore;
using MovieWebApi.Models;

namespace MovieWebApi.Data
{
    public class MovieAppDbContext : DbContext
    {
      public MovieAppDbContext(DbContextOptions options) : base(options) { }
      public  DbSet<MovieModel> Movies { get; set; }
      public  DbSet<GenreModel> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieAppDbContext).Assembly);
        }


    }
}
