using Microsoft.EntityFrameworkCore;
using MovieWebApi.Interfaces;
using MovieWebApi.Models;

namespace MovieWebApi.Data
{
    public class MovieRepository : IMovieApp<MovieModel>
    {
        private readonly MovieAppDbContext _dbContext;
        public MovieRepository(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<MovieModel> CreateAsync(MovieModel entity)
        {
           
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Movies.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dbContext.Movies.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _dbContext.Movies.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<GetAllMovieDto>> GetAllAsyncJoinId()
        {
            return await _dbContext.Movies.Include(m=>m.Genre).Select(q=>new GetAllMovieDto
            {
                Description = q.Description,
                Title = q.Title,
                ImdbRate = q.ImdbRate,  
                GenreTitle=q.Genre.Title
            }).ToListAsync();
        }

        public async Task<GetAllMovieDto> GetByIdAsyncJoin(int id)
        {
            return await _dbContext.Movies.Where(m => m.Id == id).Select(q => new GetAllMovieDto
            {
                Description = q.Description,
                Title = q.Title,
                ImdbRate = q.ImdbRate,
                GenreTitle = q.Genre.Title
            }).FirstOrDefaultAsync();
        }

        public async Task<MovieModel> UpdateAsync(int id, MovieModel entity)
        {
            var existingMovie = await _dbContext.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null)
                return null;

            _dbContext.Movies.Entry(existingMovie).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return existingMovie;
        }

        Task<IEnumerable<MovieModel>> IMovieApp<MovieModel>.GetAllAsyncJoin()
        {
            throw new NotImplementedException();
        }

        Task<MovieModel> IMovieApp<MovieModel>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
