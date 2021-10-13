using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson4
{
    static class RandomExtension
    {
        public static string NextString(this Random rand, int length)
        {
            const string chars = "йцукенгшщзхфывапролдячсмитьбю";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rand.Next(s.Length)])
              .ToArray());
        }
    }
}
