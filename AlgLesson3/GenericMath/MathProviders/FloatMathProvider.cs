using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlgLesson3
{
    public sealed class FloatMathProvider : IMathProvider<float>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Add(float a, float b) => a + b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Divide(float a, float b) => a / b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Multiply(float a, float b) => a * b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Negate(float a) => -a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Subtract(float a, float b) => a - b;
    }
}
