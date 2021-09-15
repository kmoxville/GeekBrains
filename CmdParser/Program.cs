using System;

namespace CmdParser
{
    class Options
    {
        [Option("-v", "--verbose", HelpText = "Verbose output")]
        public bool Verbose { get; set; }

        [Option("-p", "--period", HasValue = true, HelpText = "Period, 5 seconds default")]
        public int Period { get; set; }

        [OptionsDescription]
        public string Description { get; set; } = "Interactive process manager";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();
            Parser parser = new Parser(args);
            parser.Parse(options);
        }
    }
}
