using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NgTemplate.API.Data;
using NgTemplate.API.DTOs.Enums;

namespace NgTemplate.API.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity>
                                                            where TEntity : class
    {
        private readonly AppDBContext _context;
        public GenericRepository(AppDBContext context)
        {
            this._context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate)?.ToListAsync();
        }

        public async Task<ResourceOperationResult> DeleteAsync(int id)
        {
            var entity = _context.Set<TEntity>().FindAsync(id);
            if (entity is { })
            {
                _context.Entry(entity).State = EntityState.Deleted;
                try
                {
                    await _context.SaveChangesAsync();
                    return ResourceOperationResult.Success;
                }
                catch
                {
                    return ResourceOperationResult.Failure;
                }
            }
            return ResourceOperationResult.NotFound;
        }
    }
}