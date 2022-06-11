using Application.Interfaces.Data;
using Data.Context;
using Shared.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Data.Common
{
    public class Repository<T> : IRepository<T> where T : BaseContextEntity, new()
    {
        protected readonly IDbContext Context;
        public Repository(IDbContext context)
        {
            Context = context;
        }
        protected DbSet<T> Table { get => Context.Set<T>(); }
        public IQueryable<T> GetList() => Table.AsQueryable();
        public IQueryable<T> GetListNoTracking() => Table.AsNoTracking();
        public async Task<T> InsertT(T entity)
        {
            await Table.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
        public async Task Insert(T entity)
        {
            await Table.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        public async Task Insert(List<T> entity)
        {
            await Table.AddRangeAsync(entity);
            await Context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            Table.Update(entity);
            Context.SaveChangesAsync();
        }
        public void Update(List<T> entity)
        {
            Table.UpdateRange(entity);
            Context.SaveChangesAsync();
        }
    }
}
