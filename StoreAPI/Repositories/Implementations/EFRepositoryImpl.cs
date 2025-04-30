using Microsoft.EntityFrameworkCore;
using StoreAPI.Context;
using StoreAPI.Models;
using System.Linq.Expressions;

namespace StoreAPI.Repositories.Implementations
{
    public class EFRepositoryImpl<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public EFRepositoryImpl(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> FindByIdAsync(long id) =>
            await _dbSet.FindAsync(id);

        public IQueryable<T> Query() => _dbSet.AsQueryable();

        public async Task CreateAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Delete(T entity) =>
            _dbSet.Remove(entity);

        public Task<int> SaveChangesAsync() =>
            _context.SaveChangesAsync();
    }
}
