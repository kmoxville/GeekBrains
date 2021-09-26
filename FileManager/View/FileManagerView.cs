using CmdParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    public class FileManagerView
    {
        private readonly FileManagerModel _fileManagerModel;
        private int _currentPage;
        private int _width = 100;
        private int _height = 30;
        private int _cursorPosition;
        private List<char> _userInput = new List<char>();
        private string _lastError;
        private History _history;
        private List<FileManagerAction> _actions = new List<FileManagerAction>();

        public FileManagerView(FileManagerModel fileManagerModel, History history)
        {
            if (fileManagerModel == null) throw new ArgumentNullException("fileManagerModel");
            if (history == null) throw new ArgumentNullException("history");

            _fileManagerModel = fileManagerModel;
            _history = history;

            _actions.Add(new FileManagerAction("cd", new ChangeDirCommand(), new ChangeDirCommandOptions()));
            _actions.Add(new FileManagerAction("cp", new CopyCommand(), new CopyCommandOptions()));
            _actions.Add(new FileManagerAction("rm", new RemoveCommand(), new RemoveCommandOptions()));
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = (value < 0 ? 0 : value);
                _currentPage = (_height * _currentPage > _fileManagerModel.Content.Length) ? _fileManagerModel.Content.Length / _height : _currentPage;
            }
        }

        public int Width
        {
            get => _width;
            set
            {
                if (value < 0) throw new ArgumentException("Width must be > 0");

                _width = value;
                _width = (_width < 100 ? 100 : _width);
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                if (value < 0) throw new ArgumentException("Height must be > 0");

                _height = value;
                _height = (_height < 20 ? 20 : _height);
            }
        }

        private int CursorPosition
        {
            get => _cursorPosition;
            set
            {
                _cursorPosition = (value > _userInput.Count ? _userInput.Count : value);
                _cursorPosition = (_cursorPosition < 0 ? 0 : _cursorPosition);
            }
        }

        public void Run()
        {
            Action Loop = delegate
            {
                do
                {
                    UpdateScreen();

                    ConsoleKeyInfo cki = Console.ReadKey();
                    switch (cki.Key)
                    {
                        case ConsoleKey.PageDown:
                            NextPage();
                            break;
                        case ConsoleKey.PageUp:
                            PrevPage();
                            break;
                        case ConsoleKey.LeftArrow:
                            CursorPosition--;
                            break;
                        case ConsoleKey.RightArrow:
                            CursorPosition++;
                            break;
                        case ConsoleKey.Enter:
                            if (!TryExecuteCommand(string.Concat(_userInput)))
                                return;
                            
                            break;
                        case ConsoleKey.UpArrow:
                            _userInput = _history.Prev().Select(a => a).ToList();
                            CursorPosition = _userInput.Count + 1;
                            break;
                        case ConsoleKey.DownArrow:
                            _userInput = _history.Next().Select(a => a).ToList();
                            CursorPosition = _userInput.Count + 1;
                            break;
                        case ConsoleKey.Backspace:
                            CursorPosition--;
                            if (CursorPosition < _userInput.Count)
                                _userInput.RemoveAt(CursorPosition);

                            break;
                        default:
                            if (cki.KeyChar != '\u0000')
                            {
                                try
                                {
                                    _userInput.Insert(CursorPosition, cki.KeyChar);
                                }
                                catch
                                {
                                    _userInput.Add(cki.KeyChar);
                                }

                                CursorPosition++;
                            }

                            break;
                    }
                } while (true);
            };

            Loop();
        }

        private void Exit()
        {
            
        }

        private bool TryExecuteCommand(string userInput)
        {
            bool result = true;

            _history.Add(userInput);
            _userInput.Clear();
            CursorPosition = 0;

            try
            {
                result = ExecuteCommand(userInput);
            }
            catch (DirectoryNotFoundException ex)
            {
                _lastError = ex.Message;
            }
            catch (FileConflictException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("File conflicts detected:");
                foreach (var conflict in ex.FileConflicts)
                {
                    sb.AppendLine($"\t{conflict.First.FullName} <----> {conflict.Second.FullName}");
                }

                _lastError = sb.ToString();
            }
            catch (DirectoryNotEmptyException ex)
            {
                _lastError = "Directory not empty: " + ex.Path;
            }
            catch
            {
                _lastError = "Unknown error";
            }

            return result;
        }

        private bool ExecuteCommand(string userInput)
        {
            _lastError = "";

            string[] tokens = userInput.Trim().Split();
            if (tokens.Length == 0)
                return true;

            string cmd = tokens[0];

            if (cmd == "exit")
            {
                return false;
            }

            FileManagerAction action = _actions.Where(action => cmd == action.CommandName).FirstOrDefault();

            if (action != null)
            {
                _lastError = action.Execute(_fileManagerModel, tokens.Skip(1).ToArray());
            }
            else
            {
                _lastError = $"Unknown command: {cmd}";
            }

            return true;
        }

        private void NextPage()
        {
            CurrentPage++;
        }

        private void PrevPage()
        {
            CurrentPage--;
        }

        private void UpdateScreen()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Current directory: ").AppendLine(_fileManagerModel.CurrentDirectory.FullName);

            sb.AppendLine(new string('-', _width));

            var directoryContentQuery = _fileManagerModel.Content.Skip(_height * CurrentPage).Take(_height);
            sb.AppendLine("Path".PadRight(60) + "Creation time".PadRight(30) + "Size, Mb");
            foreach (FileSystemInfo fsi in directoryContentQuery)
            {
                sb.Append(fsi.Name.PadRight(60));
                sb.Append(fsi.CreationTime.ToString().PadRight(30));
                if (fsi is FileInfo fi)
                {
                    sb.Append(Math.Round(fi.Length / 1_000_000.0, 3).ToString().PadRight(20));
                }

                sb.AppendLine();
            }

            if (!string.IsNullOrEmpty(_lastError))
            {
                sb.AppendLine(new string('-', _width));
                sb.AppendLine(_lastError);
            }

            sb.AppendLine(new string('-', _width));

            sb.Append(":" + string.Concat(_userInput));

            Console.Clear();
            Console.Write(sb.ToString());
            Console.CursorLeft = CursorPosition + 1;
        }
    }
}
