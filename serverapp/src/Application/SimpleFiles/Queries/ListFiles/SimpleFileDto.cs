using System;

namespace SimpleFileUpload.Application.SimpleFiles.Queries.ListFiles
{
    public class SimpleFileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public decimal FileSizeInMb { get; set; }
    }
}