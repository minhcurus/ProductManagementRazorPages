using Microsoft.EntityFrameworkCore;
using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MyStoreContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MyStoreContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(e => EF.Property<string>(e, "EmailAddress") == name);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true)
        {
            IQueryable<T> query = _dbSet;

            // Luôn nối với Category nếu là Product
            if (typeof(T) == typeof(Product))
            {
                query = query.Include("Category");
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            int totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (items, totalCount);
        }
    }

}
