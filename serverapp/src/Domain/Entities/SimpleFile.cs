using System;
using SimpleFileUpload.Domain.Interfaces;

namespace SimpleFileUpload.Domain.Entities
{
    public class SimpleFile : IEntity
    {
        public Guid Id { get; }
        public string FileName { get; }
        public DateTime UploadDate { get; }
        public byte[] Content { get; }
        public int ContentSize { get; }
        public string ContentType { get; }

        private SimpleFile()
        {
        }

        public SimpleFile(string fileName, DateTime uploadDate, byte[] content, int contentSize, string contentType)
            : this()
        {
            Id = Guid.NewGuid();
            FileName = fileName;
            UploadDate = uploadDate;
            Content = content;
            ContentSize = contentSize;
            ContentType = contentType;
        }

        public SimpleFile(Guid id, string fileName, DateTime uploadDate, byte[] content, int contentSize, string contentType)
        {
            Id = id;
            FileName = fileName;
            UploadDate = uploadDate;
            Content = content;
            ContentSize = contentSize;
            ContentType = contentType;
        }
    }
}