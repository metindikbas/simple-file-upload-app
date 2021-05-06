using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleFileUpload.Domain.Entities;

namespace SimpleFileUpload.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<SimpleFile> SimpleFiles { get; set; }
        DbSet<UploadSetting> UploadSettings { get; set; }
    }
}