using System;

namespace SimpleFileUpload.Application.Common.Exceptions
{
    public class UnsupportedMediaTypeException : Exception
    {
        public UnsupportedMediaTypeException(string fileName) : base($"File '{fileName}' has unsupported type!")
        {
        }
    }
}