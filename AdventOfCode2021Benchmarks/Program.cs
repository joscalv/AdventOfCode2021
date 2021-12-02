using BenchmarkDotNet.Running;

namespace AdventOfCode2020Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<AdventOfCodeBenchmarks>();
        }
    }
}