using BenchmarkDotNet.Running;
using System;

namespace AlgLesson3
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DistanceBenchmark>();
            //DistanceBenchmark db = new DistanceBenchmark();
            //db.SetupData();
            //db.TestCasualDoubleDistance();
        }
    }
}
