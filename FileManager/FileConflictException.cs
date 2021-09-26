using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace FileManager
{
    public sealed class FileConflict
    {
        public FileConflict(FileSystemInfo first, FileSystemInfo second)
        {
            First = first;
            Second = second;
        }

        public FileSystemInfo First { get; private set; }
        public FileSystemInfo Second { get; private set; }
    }

    [Serializable]
    public class FileConflictException : CommandException
    {
        public List<FileConflict> FileConflicts { get; private set; }

        public FileConflictException(List<FileConflict> fileConflicts)
        {
            if ((fileConflicts?.Count ?? 0) == 0) throw new ArgumentException("fileConflicts is null or empty");

            FileConflicts = fileConflicts;
        }

        protected FileConflictException(SerializationInfo info, StreamingContext context) 
            : base(info, context) 
        { 

        }
    }
}
