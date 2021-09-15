using System;
using System.Diagnostics;
using System.Timers;
using System.Linq;
using System.Text;

namespace TaskManager
{
    public class TaskManager
    {
        public TaskManager(int period = 5, int maximumRowsOnPage = 20)
        {
            Period              = period;
            MaximumRowsOnPage   = maximumRowsOnPage;
            CurrentPage         = 1;
            _loopWorker         = StandardLoopWorker;
        }

        public int Period              
        {
            get => _period;
            private set
            {
                if (value < 1)
                    throw new ArgumentException("Period must be 1 seconds or higher");

                _period = value * 1000;
            }
        }

        public int MaximumRowsOnPage 
        {
            get => _maximumRowsOnPage;
            private set
            {
                if (value < 5)
                    throw new ArgumentException("There have to be at least 5 rows on screen");

                _maximumRowsOnPage = value;
            }
        }

        public int CurrentPage 
        { 
            get => _currentPage; 
            private set
            {
                _currentPage = (value < 1 ? 1 : value);
            }
        }

        public string LastError { get; private set; } = string.Empty;

        /// <summary>
        /// Два делегата - стандартный обработчик команд и обработчик команды kill(ожидает ввода id)
        /// Вместо лесенки if-оф
        /// </summary>
        public void Start()
        {
            UpdateScreen();

            timer = new Timer(Period);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            while (_loopWorker())
            {
                
            }
        }

        #region LoopExecutors
        private bool StandardLoopWorker()
        {
            bool continueExecution  = true;
            ConsoleKeyInfo ki       = Console.ReadKey();
            Command cmd             = GetCommand(ki.KeyChar);

            LastError = (cmd == Command.Unrecognised ? $"Unknown command {ki.KeyChar}" : string.Empty); 
            
            if (cmd == Command.Quit)
            {
                QuitCommand();
                continueExecution = false;
            }
            else if (cmd == Command.PreviousPage)
            {
                PreviousPageCommand();
            }
            else if (cmd == Command.NextPage)
            {
                NextPageCommand();
            }
            else if (cmd == Command.Kill)
            {
                KillProcessCommand();
            }

            UpdateScreen();

            return continueExecution;
        }

        //Console.Clear стирает ввод, использование Console.Read невозможно
        private bool KillLoopWorker()
        {
            ConsoleKeyInfo ki;
            while ((ki = Console.ReadKey()).KeyChar != '\r')
            {
                _currentUserInput += ki.KeyChar;
                _explanationString = $"Process ID to kill: {_currentUserInput}";
            }

            if (int.TryParse(_currentUserInput, out int processId))
            {
                Process p = Process.GetProcessById(processId);
                p.Kill();
            }
            else
            {
                LastError = $"Invalid id: {_currentUserInput}";
            }

            _currentUserInput   = string.Empty;
            _explanationString  = string.Empty;
            _loopWorker         = StandardLoopWorker;

            UpdateScreen();

            return true;
        }
        #endregion

        #region Commands
        private void KillProcessCommand()
        {
            _explanationString  = $"Process ID to kill: ";
            _loopWorker         = KillLoopWorker;
        }

        private void NextPageCommand()
        {
            CurrentPage++;
        }

        private void PreviousPageCommand()
        {
            CurrentPage--;
        }

        private void QuitCommand()
        {
            timer.Stop();
        }
        #endregion

        private Command GetCommand(char keyChar) => char.ToUpper(keyChar) switch
        {
            'K' => Command.Kill,
            'N' => Command.NextPage,
            'P' => Command.PreviousPage,
            'Q' => Command.Quit,
            _ => Command.Unrecognised
        };

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateScreen();
        }

        private void UpdateScreen()
        {
            Console.Clear();

            Process[] processes = Process.GetProcesses();
            int totalCount = processes.Length;

            var processQuery = processes
                .Skip((CurrentPage - 1) * MaximumRowsOnPage)
                .Take(MaximumRowsOnPage)
                .Select((process, index) => new ProcessRow() 
                { 
                    Index = index + ((CurrentPage - 1) * MaximumRowsOnPage) + 1, 
                    Name = process.ProcessName, 
                    Id = process.Id
                });

            Console.WriteLine("Process name".PadLeft(43) + "Id".PadLeft(15));
            Console.WriteLine(new string('_', 60));

            foreach (ProcessRow pr in processQuery)
            {
                Console.WriteLine(pr);
            }

            Console.WriteLine($"Current page: {CurrentPage}");

            if (LastError != string.Empty)
                Console.WriteLine(LastError);

            if (_explanationString != string.Empty)
                Console.Write(_explanationString);
            else
                Console.WriteLine("K [ID] - kill process by ID; N - next page; P - previous page; Q - quit");
        }

        private delegate bool LoopWorker();

        private enum Command
        {
            Kill,
            PreviousPage,
            NextPage,
            Quit,
            Unrecognised
        }

        private class ProcessRow
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public int Index { get; set; }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append((Index + ")").PadRight(3)) //грязь?
                    .Append(Name.PadLeft(40).Substring(0, 40)) //
                    .Append(Id.ToString().PadLeft(15)); //

                return sb.ToString();
            }
        }

        private int _period;
        private int _maximumRowsOnPage;
        private int _currentPage;
        private string _explanationString   = string.Empty;
        private string _currentUserInput    = string.Empty;
        private Timer timer;
        private LoopWorker _loopWorker;
    }
}
