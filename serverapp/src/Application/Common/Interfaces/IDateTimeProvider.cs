using System;

namespace SimpleFileUpload.Application.Common.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}