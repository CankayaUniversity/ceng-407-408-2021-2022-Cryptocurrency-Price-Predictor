using System.Data;
using Application.Api;
using Application.Domain;
using Shared.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class ProjectDbContext : DbContext, IDbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ProjectConfiguration.Configuration.GetConnectionString("DbConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration<>).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<T> Set<T>() where T : BaseContextEntity
        {
            return base.Set<T>();
        }

        public async Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolation = IsolationLevel.Unspecified) => await Database.BeginTransactionAsync();
        public DbSet<User> User { get; set; }
        public DbSet<ModelPrediction> ModelPrediction { get; set; }
        
    }
}
