using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day11Test
    {

        [Fact]
        public void TestPart1()
        {
            Day11 day = new();
            var expectedSolution = 1632;
            day.ExecutePart1().Should().Be(expectedSolution);

        }
        
        [Fact]
        public void TestPart1Sample()
        {
            string input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";
            var expectedSolution = 1656;
            Day11.RunSteps(input.ParseInputDay11()).totalFlases.Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day11 day = new();
            var expectedSolution = 303L;
            day.ExecutePart2().Should().Be(expectedSolution);

        }
    }
}