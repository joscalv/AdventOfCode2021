using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day05Test
    {
        private readonly Day05 _day05 = new Day05();

        private readonly Line[] _sample = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2"
            .Split('\n')
            .Select(Day05Extensions.ParseLine)
            .ToArray();

        [Fact]
        public void TestPart1()
        {
            var expectedSolution = 5585;
            var problem1Result = _day05.ExecutePart1();
            problem1Result.Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart1Sample()
        {
            var expectedSolution = 5;
            var problem1Result = _day05.ExecutePart1(_sample);
            problem1Result.Should().Be(expectedSolution);

        }
        
        [Fact]
        public void TestPart2Sample()
        {
            var expectedSolution = 12;
            var problem1Result = _day05.ExecutePart2(_sample);
            problem1Result.Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart2()
        {
            var expectedSolution = 17193;
            var problem1Result = _day05.ExecutePart2();
            problem1Result.Should().Be(expectedSolution);
        }

        [Fact]
        public void GetLinePointsTest()
        {
            new Line(1, 1, 1, 3).GetLinePoints().Should().BeEquivalentTo(
                new List<Point>
                {
                    new(1, 1),
                    new(1, 2),
                    new(1, 3),
                });

            new Line(9, 7, 7, 7).GetLinePoints().Should().BeEquivalentTo(
                new List<Point>
                {
                    new(9, 7),
                    new(8, 7),
                    new(7, 7),
                });
            new Line(1, 1, 3, 3).GetLinePoints().Should().BeEquivalentTo(
                new List<Point>
                {
                    new(1, 1),
                    new(2, 2),
                    new(3, 3),
                });
            new Line(9, 7, 7, 9).GetLinePoints().Should().BeEquivalentTo(
                new List<Point>
                {
                    new(9, 7),
                    new(8, 8),
                    new(7, 9),
                });
        }
    }
}