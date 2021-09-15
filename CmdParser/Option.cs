using System;

namespace CmdParser
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class Option : Attribute
    {
        public Option(string shortName, string fullName)
        {
            ShortName = shortName;
            FullName = fullName;
        }

        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string HelpText { get; set; } = string.Empty;
        public bool Required { get; set; } = false;
        public bool HasValue { get; set; } = false;
    }
}
