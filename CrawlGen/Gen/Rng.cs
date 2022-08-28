﻿namespace CrawlGen.Gen
{
    public static class Rng
    {
        // TODO: ThreadLocal
        // TODO: UnitTest Mocking
        static readonly Random Random = new();

        public static int UniformInt(int max = int.MaxValue) => Random.Next(max);
        public static int UniformInt(int min, int max) => Random.Next(min, max);
        internal static bool P(double v) => Random.NextDouble() < v;
        internal static T TakeOne<T>(IList<T> list)=> list[Random.Next(list.Count)];
        internal static int D(int v) => Random.Next(v) + 1;

        internal static int Round(double val) => (int)Math.Floor(val + Random.NextDouble());

        internal static double UniformDouble(double max = 1) => Random.NextDouble() * (max);
        internal static double UniformDouble(double min, double max) => Random.NextDouble() * (max - min) + min;

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
