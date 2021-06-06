using System;
using System.Collections.Generic;

namespace AfterMe.Core.InternalLib
{
    /// <summary>
    /// Represents the application exception, so it will be known exception in higher level layer
    /// </summary>
    public class AppException : ApplicationException
    {
        public List<object> Errors { get; set; }

        public AppException()
        {
            Errors = new List<object>();
        }
        public AppException(string message) : base(message)
        {
            Errors = new List<object>();
        }
    }

    /// <summary>
    /// This exceptio will be thrown if a record is not found.
    /// </summary>
    public class NotFoundException : AppException
    {
        public NotFoundException() : base("Record not found")
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// This exceptio will be thrown if a record is not found.
    /// </summary>
    public class DuplicateRecordException : AppException
    {
        public DuplicateRecordException() : base("Duplicate record")
        {
        }
        public DuplicateRecordException(string message) : base(message)
        {
        }
    }
}
