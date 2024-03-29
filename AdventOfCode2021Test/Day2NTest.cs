using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day2NTest
    {

        [Fact]
        public void TestPart1()
        {
            Day2N day = new();
            var expectedSolution = 0;
            day.ExecutePart1().Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day2N day = new();
            var expectedSolution = 0;
            day.ExecutePart2().Should().Be(expectedSolution);

        }
    }
}