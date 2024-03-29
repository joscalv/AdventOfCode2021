﻿using AdventOfCode2021;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2021Benchmarks
{
    [MarkdownExporterAttribute.GitHub]
    [HtmlExporter]
    [MemoryDiagnoser]
    public class AdventOfCodeBenchmarks
    {
        private static readonly Day01 Day01 = new();
        private static readonly Day02 Day02 = new();
        private static readonly Day03 Day03 = new();
        private static readonly Day04 Day04 = new();
        private static readonly Day05 Day05 = new();
        private static readonly Day06 Day06 = new();
        private static readonly Day07 Day07 = new();
        private static readonly Day08 Day08 = new();
        private static readonly Day09 Day09 = new();
        private static readonly Day10 Day10 = new();
        private static readonly Day11 Day11 = new();
        private static readonly Day12 Day12 = new();
        private static readonly Day13 Day13 = new();
        private static readonly Day14 Day14 = new();


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
        public void Day05_1() => Day05.ExecutePart1();

        [Benchmark]
        public void Day05_2() => Day05.ExecutePart2();

        [Benchmark]
        public void Day06_1() => Day06.ExecutePart1();

        [Benchmark]
        public void Day06_2() => Day06.ExecutePart2();

        [Benchmark]
        public void Day07_1() => Day07.ExecutePart1();

        [Benchmark]
        public void Day07_2() => Day07.ExecutePart2();

        [Benchmark]
        public void Day08_1() => Day08.ExecutePart1();

        [Benchmark]
        public void Day08_2() => Day08.ExecutePart2();

        [Benchmark]
        public void Day09_1() => Day09.ExecutePart1();

        [Benchmark]
        public void Day09_2() => Day09.ExecutePart2();

        [Benchmark]
        public void Day10_1() => Day10.ExecutePart1();

        [Benchmark]
        public void Day10_2() => Day10.ExecutePart2();

        [Benchmark]
        public void Day11_1() => Day11.ExecutePart1();

        [Benchmark]
        public void Day11_2() => Day11.ExecutePart2();

        [Benchmark]
        public void Day12_1() => Day12.ExecutePart1();

        [Benchmark]
        public void Day12_2() => Day12.ExecutePart2();

        [Benchmark]
        public void Day13_1() => Day13.ExecutePart1();

        [Benchmark]
        public void Day13_2() => Day13.ExecutePart2();

        [Benchmark]
        public void Day14_1() => Day14.ExecutePart1();

        [Benchmark]
        public void Day14_2() => Day14.ExecutePart2();

    }
}
