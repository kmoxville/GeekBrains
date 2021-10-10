using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    [MaxColumn]
    public class DistanceBenchmark
    {
        private PointClass<double>[] _doublePoints;
        private PointClass<float>[] _floatPoints;
        private PointStruct<double>[] _doublePointStructs;
        private PointStruct<float>[] _floatPointStructs;

        private PointCasualDouble[] _doubleCasualPoints;
        private PointCasualFloat[] _floatCasualPoints;

        private Random _rand = new Random();

        [GlobalSetup]
        public void SetupData()
        {
            Random rand = new Random();

            //generic
            _doublePoints = new PointClass<double>[100000];
            for (int i = 0; i < _doublePoints.Length; i++)
            {
                _doublePoints[i] = new PointClass<double>(x: rand.GetRandomDouble(0, 10), y: rand.GetRandomDouble(0, 10));
            }

            _floatPoints = new PointClass<float>[100000];
            for (int i = 0; i < _floatPoints.Length; i++)
            {
                _floatPoints[i] = new PointClass<float>(x: rand.GetRandomFloat(0, 10), y: rand.GetRandomFloat(0, 10));
            }

            _doublePointStructs = new PointStruct<double>[100000];
            for (int i = 0; i < _doublePointStructs.Length; i++)
            {
                _doublePointStructs[i] = new PointStruct<double>(x: rand.GetRandomDouble(0, 10), y: rand.GetRandomDouble(0, 10));
            }

            _floatPointStructs = new PointStruct<float>[100000];
            for (int i = 0; i < _floatPointStructs.Length; i++)
            {
                _floatPointStructs[i] = new PointStruct<float>(x: rand.GetRandomFloat(0, 10), y: rand.GetRandomFloat(0, 10));
            }

            //non generic
            _doubleCasualPoints = new PointCasualDouble[100000];
            for (int i = 0; i < _doubleCasualPoints.Length; i++)
            {
                _doubleCasualPoints[i] = new PointCasualDouble(x: rand.GetRandomDouble(0, 10), y: rand.GetRandomDouble(0, 10));
            }

            _floatCasualPoints = new PointCasualFloat[100000];
            for (int i = 0; i < _floatCasualPoints.Length; i++)
            {
                _floatCasualPoints[i] = new PointCasualFloat(x: rand.GetRandomFloat(0, 10), y: rand.GetRandomFloat(0, 10));
            }
        }

        [Benchmark(Description = "Generic, class, double, уточненная версия дженерика")]
        public void TestDoubleDistance()
        {
            var doubleMathProvider = new DoubleMathProvider();

            for (int i = 0; i < _doublePoints.Length - 2; i += 2)
            {
                Math.CountDistance(_doublePoints[i], _doublePoints[i + 1]);
            }
        }

        [Benchmark(Description = "Generic, class, float")]
        public void TestFloatDistance()
        {
            var floatMathProvider = new FloatMathProvider();
            for (int i = 0; i < _floatPoints.Length - 2; i += 2)
            {
                Math.CountDistance(_floatPoints[i], _floatPoints[i + 1], floatMathProvider);
            }
        }

        [Benchmark(Description = "Generic, struct, double")]
        public void TestDoubleStructDistance()
        {
            var doubleMathProvider = new DoubleMathProvider();

            for (int i = 0; i < _doublePointStructs.Length - 2; i += 2)
            {
                Math.CountDistance(_doublePointStructs[i], _doublePointStructs[i + 1], doubleMathProvider);
            }
        }

        [Benchmark(Description = "Generic, struct, float")]
        public void TestFloatStructDistance()
        {
            var floatMathProvider = new FloatMathProvider();

            for (int i = 0; i < _floatPointStructs.Length - 2; i += 2)
            {
                Math.CountDistance(_floatPointStructs[i], _floatPointStructs[i + 1], floatMathProvider);
            }
        }

        //non generic
        [Benchmark(Description = "class, double")]
        public void TestCasualDoubleDistance()
        {
            for (int i = 0; i < _doubleCasualPoints.Length - 2; i += 2)
            {
                CasualMath.CountDistance(_doubleCasualPoints[i], _doubleCasualPoints[i + 1]);
            }
        }

        [Benchmark(Description = "class, float")]
        public void TestCasualFloatDistance()
        {
            for (int i = 0; i < _floatCasualPoints.Length - 2; i += 2)
            {
                CasualMath.CountDistance(_floatCasualPoints[i], _floatCasualPoints[i + 1]);
            }
        }
    }

    static class RandomExtension
    {
        public static double GetRandomDouble(this Random random, double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static float GetRandomFloat(this Random random, float minimum, float maximum)
        {
            return (float)(random.NextDouble() * (maximum - minimum) + minimum);
        }
    }
}
