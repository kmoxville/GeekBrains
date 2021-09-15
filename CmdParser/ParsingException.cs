using System;
using System.Collections.Generic;
using System.Text;

namespace CmdParser
{
    public class ParsingException : Exception
    {
        public ParsingException()
        {
        }

        public ParsingException(string message)
            : base(message)
        {
        }

        public ParsingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public string Argument { get; set; }
    }
}
