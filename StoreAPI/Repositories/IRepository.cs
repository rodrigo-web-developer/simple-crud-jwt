namespace StoreAPI.Repositories
{
    using StoreAPI.Models;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : IEntity
    {
        Task<T> FindByIdAsync(long id);
        IQueryable<T> Query();

        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<int> SaveChangesAsync();
    }
}
