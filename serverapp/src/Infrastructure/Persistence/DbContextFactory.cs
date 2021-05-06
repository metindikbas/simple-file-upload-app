using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SimpleFileUpload.Infrastructure.Persistence
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Default");
            if (string.IsNullOrEmpty(connectionString))
            {
                var rootPath = Path.GetRelativePath(Directory.GetCurrentDirectory(), "../");
                var appSettingFiles =
                    Directory.GetFiles(rootPath, "appsettings.Development.json", SearchOption.AllDirectories);
                var confBuilder = new ConfigurationBuilder();
                confBuilder.AddJsonFile(Path.GetFullPath(appSettingFiles.First()));
                var config = confBuilder.Build();
                connectionString = config.GetConnectionString("Default");
            }

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            return new ApplicationDbContext(builder.Options);
        }
    }
}