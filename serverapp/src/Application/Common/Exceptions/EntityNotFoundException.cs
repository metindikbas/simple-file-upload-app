using System;

namespace SimpleFileUpload.Application.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, object key) : base(
            $"Entity '{entityName}' with id {key} not found!")
        {
        }
    }
}