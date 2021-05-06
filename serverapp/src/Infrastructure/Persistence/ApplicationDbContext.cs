using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SimpleFileUpload.Application.Common.Interfaces;
using SimpleFileUpload.Domain.Entities;

namespace SimpleFileUpload.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<SimpleFile> SimpleFiles { get; set; }
        public DbSet<UploadSetting> UploadSettings { get; set; }
    }
}