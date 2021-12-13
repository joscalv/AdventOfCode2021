using AdventOfCode2021;
using FluentAssertions;
using System;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day13Test
    {

        [Fact]
        public void TestPart1()
        {
            Day13 day = new();
            var expectedSolution = 669;
            day.ExecutePart1().Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day13 day = new();
            var expectedSolution = 90;
            day.ExecutePart2().Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart1Sample1()
        {
            var sampleInput = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5".ReplaceLineEndings().Split(Environment.NewLine);

            var sampleValues = Day13.Day13Extensions.ParseInput(sampleInput);
            var expectedSolution = 16;
            Day13.Fold(sampleValues.coordinates, sampleValues.instructions).Length.Should().Be(expectedSolution);

        }
    }
}