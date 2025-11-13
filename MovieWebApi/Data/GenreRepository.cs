using Microsoft.EntityFrameworkCore;
using MovieWebApi.Interfaces;
using MovieWebApi.Models;

namespace MovieWebApi.Data
{
    public class GenreRepository : IMovieApp<GenreModel>
    {
        private readonly MovieAppDbContext _dbContext;
        public GenreRepository(MovieAppDbContext context)
        {
            _dbContext=context;
        }
        public async Task<GenreModel> CreateAsync(GenreModel entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var SelectedGenre =await _dbContext.Genres.FindAsync(id);
            if (SelectedGenre==null)
            {
                return false;
            }
            _dbContext.Genres.Remove(SelectedGenre);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var SelectedGenre = await _dbContext.Genres.FindAsync(id);
            if (SelectedGenre == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<GenreModel>> GetAllAsyncJoin()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        public async Task<GenreModel> GetByIdAsync(int id)
        {
            return await _dbContext.Genres.FindAsync(id);
        }

        public async Task<GenreModel> UpdateAsync(int id, GenreModel entity)
        {
            var SelectedMovie = await _dbContext.Genres.FirstOrDefaultAsync(m =>m.Id == id);
            if (SelectedMovie==null)
            {
                return null;
            }
           _dbContext.Genres.Entry(SelectedMovie).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return SelectedMovie;
        }
    }
}
