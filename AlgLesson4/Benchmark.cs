using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson4
{
    [MaxColumn]
    public class Benchmark
    {
        private const int SIZE = 10000;
        private const int STRING_LENGTH = 10;
        private string[] _array;
        private HashSet<string> _hashSet;

        [GlobalSetup]
        public void SetupData()
        {
            Random rand = new Random();
            _array = new string[SIZE];
            _hashSet = new();

            for (int i = 0; i < _array.Length; i++)
            {
                string randString = rand.NextString(STRING_LENGTH);
                _array[i] = randString;
                _hashSet.Add(randString);
            }
        }

        [Benchmark(Description = "Search in array")]
        public int TestArraySearch()
        {
            string strToSearch =_array[SIZE - 1];

            return Array.IndexOf<string>(_array, strToSearch);
        }

        [Benchmark(Description = "Search in hash set")]
        public bool TestHashSetSearch()
        {
            string strToSearch = _array[SIZE - 1];

            return _hashSet.Contains(strToSearch);
        }
    }
}
