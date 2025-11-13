namespace MovieWebApi.Interfaces
{
    public interface IMovieApp<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsyncJoin();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
