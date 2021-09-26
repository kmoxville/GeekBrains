using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    [Serializable]
    public class DirectoryNotEmptyException : ApplicationException
    {
        public string Path { get; set; }

        public DirectoryNotEmptyException() { }
        public DirectoryNotEmptyException(string message) { }
        public DirectoryNotEmptyException(string message, Exception inner) : base(message, inner) { }
        protected DirectoryNotEmptyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}