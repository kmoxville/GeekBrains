using BenchmarkDotNet.Running;
using System;

namespace AlgLesson4
{
    class Program
    {
        //Результат замеров:
        //|               Method |         Mean |      Error |    StdDev |          Max |                                         
        //|--------------------- |-------------:|-----------:|----------:|-------------:|                                         
        //|    'Search in array' | 51,525.01 ns | 104.321 ns | 97.582 ns | 51,753.17 ns |                                         
        //| 'Search in hash set' |     11.35 ns |   0.044 ns |  0.039 ns |     11.42 ns |
        static void Main(string[] args)
        {
            //Benchmark bench = new Benchmark();
            //bench.SetupData();
            //bench.TestArraySearch();
            //bench.TestHashSetSearch();
            BenchmarkRunner.Run<Benchmark>();
        }
    }
}
