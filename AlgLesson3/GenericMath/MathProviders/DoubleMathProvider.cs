using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    public sealed class DoubleMathProvider : IMathProvider<double>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Add(double a, double b) => a + b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Divide(double a, double b) => a / b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Multiply(double a, double b) => a * b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Negate(double a) => -a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Subtract(double a, double b) => a - b;
    }
}
