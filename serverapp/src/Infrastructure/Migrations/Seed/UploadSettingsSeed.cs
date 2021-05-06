using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleFileUpload.Application.Common.Interfaces;
using SimpleFileUpload.Domain.Entities;
using SimpleFileUpload.Domain.Enums;

namespace SimpleFileUpload.Infrastructure.Migrations.Seed
{
    public static class UploadSettingsSeed
    {
        public static async Task SeedDefaultUploadSettings(IApplicationDbContext applicationDbContext)
        {
            if (!await applicationDbContext.UploadSettings.AnyAsync(x =>
                x.Key == UploadSettingKeys.AllowedContentTypes))
            {
                var allowedContentTypes =
                    new UploadSetting(UploadSettingKeys.AllowedContentTypes, "image/png,image/jpeg");
                await applicationDbContext.UploadSettings.AddAsync(allowedContentTypes);
            }

            if (!await applicationDbContext.UploadSettings.AnyAsync(x =>
                x.Key == UploadSettingKeys.MaxAllowedSingleFileSizeInBytes))
            {
                const int maxSizeInKb = 20 * 1024; // 20 KB
                var allowedMaxSingleFileSizeInBytes =
                    new UploadSetting(UploadSettingKeys.MaxAllowedSingleFileSizeInBytes, maxSizeInKb.ToString());
                await applicationDbContext.UploadSettings.AddAsync(allowedMaxSingleFileSizeInBytes);
            }

            await applicationDbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}