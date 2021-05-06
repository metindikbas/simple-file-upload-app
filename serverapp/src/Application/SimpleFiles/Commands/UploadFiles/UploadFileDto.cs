namespace SimpleFileUpload.Application.SimpleFiles.Commands.UploadFiles
{
    public class UploadFileDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public UploadFileDto(string fileName, string contentType, byte[] content)
        {
            FileName = fileName;
            ContentType = contentType;
            Content = content;
        }
    }
}