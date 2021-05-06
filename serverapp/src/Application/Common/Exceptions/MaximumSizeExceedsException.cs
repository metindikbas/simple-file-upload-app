using System;

namespace SimpleFileUpload.Application.Common.Exceptions
{
    public class MaximumSizeExceedsException : Exception
    {
        public MaximumSizeExceedsException(string fileName) : base($"File '{fileName}' exceeds maximum allowed size!")
        {
        }
    }
}