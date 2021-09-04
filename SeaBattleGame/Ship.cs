using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattleGame
{
    struct Ship
    {
        public Ship(int size)
        {
            Size = size;
        }

        public int Size { get; set; }
    }
}
