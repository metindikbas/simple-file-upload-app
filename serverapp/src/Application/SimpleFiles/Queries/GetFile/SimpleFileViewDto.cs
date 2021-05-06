using System;

namespace SimpleFileUpload.Application.SimpleFiles.Queries.GetFile
{
    public class SimpleFileViewDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public string ContentData { get; set; }
        public decimal FileSizeInKb { get; set; }
    }
}