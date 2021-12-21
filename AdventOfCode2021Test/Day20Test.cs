using AdventOfCode2021;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day20Test
    {

        [Fact]
        public void TestPart1()
        {
            Day20 day = new();
            var expectedSolution = 5379;
            day.ExecutePart1().Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart2()
        {
            Day20 day = new();
            var expectedSolution = 17917;
            day.ExecutePart2().Should().Be(expectedSolution);
        }

        [Fact]
        public void TestPart1Sample()
        {
            var strInput = @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###".ReplaceLineEndings().Split(Environment.NewLine).ToArray();

            var input = Day20.Day20Extensions.ParseInput(strInput);

            Day20.ApplyAlgorithm(input.image, input.program, 2)
                .SelectMany(a => a)
                .Count(c => c == 1).Should().Be(35);

            Day20.ApplyAlgorithm(input.image, input.program, 50)
                .SelectMany(a => a)
                .Count(c => c == 1).Should().Be(3351);
        }
    }
}