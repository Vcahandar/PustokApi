using Microsoft.EntityFrameworkCore;
using Pustok.Core.Entities.Common;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }


        public IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }

        public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).AsQueryable();
        }

        public async Task CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public void SoftDelete(T entity)
        {

            entity.IsDeleted = true;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _table.FirstOrDefaultAsync(expression);
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
