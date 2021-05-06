using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFileUpload.Domain.Entities;

namespace SimpleFileUpload.Infrastructure.Configurations
{
    public class SimpleFileConfiguration : IEntityTypeConfiguration<SimpleFile>
    {
        public void Configure(EntityTypeBuilder<SimpleFile> builder)
        {
            builder.ToTable("Files");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedNever();
            builder.Property(p => p.FileName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.UploadDate)
                .IsRequired();
            builder.Property(p => p.Content)
                .IsRequired();
            builder.Property(p => p.ContentSize)
                .IsRequired();
            builder.Property(p => p.ContentType)
                .IsRequired();
        }
    }
}