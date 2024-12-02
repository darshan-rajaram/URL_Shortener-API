namespace URL_Shortener.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByConditionAsync(Func<T, bool> predicate);
        Task AddAsync(T entity);
        Task SaveChangesAsync();
    }

}
