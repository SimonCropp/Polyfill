#if !NET6_0_OR_GREATER

namespace Polyfills;

using System;
using System.Runtime.CompilerServices;
using System.Threading;

static partial class Polyfill
{
    static ThreadSafeRandom threadSafeRandom = new();

    sealed class ThreadSafeRandom : Random
    {
        [ThreadStatic]
        static Random? random;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static Random Create() => random = new();

        static Random LocalRandom => random ?? Create();

        public override int Next() => LocalRandom.Next();
        public override int Next(int maxValue) => LocalRandom.Next(maxValue);
        public override int Next(int minValue, int maxValue) => LocalRandom.Next(minValue, maxValue);
        public override void NextBytes(byte[] buffer) => LocalRandom.NextBytes(buffer);
        public override double NextDouble() => LocalRandom.NextDouble();
        protected override double Sample() => throw new NotSupportedException();
    }

    extension(Random)
    {
        /// <summary>
        /// Provides a thread-safe Random instance that may be used concurrently from any thread.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.random.shared?view=net-11.0
        public static Random Shared => threadSafeRandom;
    }
}
#endif
