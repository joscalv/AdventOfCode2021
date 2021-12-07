using AdventOfCode2021;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2021Benchmarks
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
        private static readonly Day05 Day05 = new Day05();
        private static readonly Day06 Day06 = new Day06();
        private static readonly Day07 Day07 = new Day07();


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

        [Benchmark]
        public void Day05_1() => Day05.ExecutePart2();

        [Benchmark]
        public void Day05_2() => Day05.ExecutePart2();

        [Benchmark]
        public void Day06_1() => Day06.ExecutePart2();

        [Benchmark]
        public void Day06_2() => Day06.ExecutePart2();

        [Benchmark]
        public void Day07_1() => Day07.ExecutePart2();

        [Benchmark]
        public void Day07_2() => Day07.ExecutePart2();

    }
}
