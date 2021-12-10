using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day10Test
    {

        [Fact]
        public void TestPart1()
        {
            Day10 day = new();
            var expectedSolution = 168417;
            day.ExecutePart1().Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day10 day = new();
            var expectedSolution = 2802519786;
            day.ExecutePart2().Should().Be(expectedSolution);

        }
    }
}