using System.Data;
using Shared.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Context
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : BaseContextEntity;
        Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolation = IsolationLevel.Unspecified);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
