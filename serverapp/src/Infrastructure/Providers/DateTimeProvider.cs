using System;
using SimpleFileUpload.Application.Common.Interfaces;

namespace SimpleFileUpload.Infrastructure.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}