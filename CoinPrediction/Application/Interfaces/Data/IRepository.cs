using Shared.Entities.Common;

namespace Application.Interfaces.Data
{
    public interface IRepository<T> where T : BaseContextEntity, new()
    {
        IQueryable<T> GetList();
        IQueryable<T> GetListNoTracking();
        Task<T> InsertT(T entity);
        Task Insert(T entity);
        Task Insert(List<T> entity);
        void Update(T entity);
        void Update(List<T> entity);
    }
}
