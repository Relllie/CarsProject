namespace WebProject.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> ListAsync();
        Task<T?> GetAsync(int id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
