using System;
using CmdParser;

namespace TaskManager
{
    class Options
    {
        [Option("-p", "--period", HasValue = true, HelpText = "Refresh interval, seconds")]
        public int Period { get; set; }

        [Option("-h", "--help", HelpText = "Prints this message")]
        public bool Help { get; set; }

        [OptionsDescription]
        public string ProgramDescription { get; set; } = "Simple interactive Task Manager";
    }

    /// <summary>
    /// Простой интерактивный такс менеджер с периодом обновления по умолчанию 5с, может быть изменен опциями командной строки
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();
            Parser parser = new Parser(args);

            parser.Parse(options);

            if (parser.HadErrors || options.Help)
            {
                Console.WriteLine(parser.Usage);
                Environment.Exit(-1);
            }

            TaskManager tm = new TaskManager(options.Period);
            tm.Start();
        }
    }
}
