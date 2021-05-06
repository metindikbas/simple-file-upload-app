using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFileUpload.Domain.Entities;
using SimpleFileUpload.Domain.Enums;

namespace SimpleFileUpload.Infrastructure.Configurations
{
    public class UploadSettingsConfiguration : IEntityTypeConfiguration<UploadSetting>
    {
        public void Configure(EntityTypeBuilder<UploadSetting> builder)
        {
            builder.ToTable("UploadSettings");
            builder.HasKey(k => k.Key);
            builder.Property(p => p.Key)
                .HasConversion(v => v.ToString(),
                    v => (UploadSettingKeys) Enum.Parse(typeof(UploadSettingKeys), v))
                .HasMaxLength(50);
            builder.Property(p => p.Value)
                .IsRequired();
        }
    }
}