using AdventOfCode2021;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2020Benchmarks
{
    [MarkdownExporterAttribute.GitHub]
    [HtmlExporter]
    [MemoryDiagnoser]
    public class AdventOfCodeBenchmarks
    {
        private static readonly Day01 Day01 = new Day01();
        private static readonly Day02 Day02 = new Day02();
        private static readonly Day03 Day03 = new Day03();
        private static readonly Day04 Day04 = new Day04();


        [Benchmark]
        public void Day01_1() => Day01.ExecutePart1();

        [Benchmark]
        public void Day01_2() => Day01.ExecutePart2();

        [Benchmark]
        public void Day02_1() => Day02.ExecutePart1();

        [Benchmark]
        public void Day02_2() => Day02.ExecutePart2();

        [Benchmark]
        public void Day03_1() => Day03.ExecutePart1();

        [Benchmark]
        public void Day03_2() => Day03.ExecutePart2();

        [Benchmark]
        public void Day04_1() => Day04.ExecutePart1();

        [Benchmark]
        public void Day04_2() => Day04.ExecutePart2();

    }
}
