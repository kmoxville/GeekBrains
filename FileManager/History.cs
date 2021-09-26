using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FileManager
{
    public class History
    {
        private int _currentPosition;

        public History(IEnumerable<string> history)
        {
            Length = 20;

            foreach (string command in history)
            {
                Add(command);
            }      
        }

        public int Length { get; private set; }
        public int CurrentPosition 
        { 
            get => _currentPosition; 
            set
            {
                _currentPosition = value;
                _currentPosition = _currentPosition > Commands.Count ? Commands.Count : _currentPosition;
                _currentPosition = _currentPosition < 0 ? 0 : _currentPosition;
            }
        }

        public List<string> Commands { get; } = new List<string>();

        public string Prev()
        {
            CurrentPosition--;
            return Commands[CurrentPosition];
        }

        public string Next()
        {
            CurrentPosition++;
            return Commands[CurrentPosition - 1];
        }

        public void Add(string command)
        {
            if (Commands.Count >= Length)
                Commands.RemoveAt(0);

            Commands.Insert(CurrentPosition, command);
            CurrentPosition++;
        }
    }
}

