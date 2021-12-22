using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day21Test
    {

        [Fact]
        public void TestPart1()
        {
            Day21 day = new();
            var expectedSolution = 518418;
            day.ExecutePart1().Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart1Sample()
        {
            Day21.PlayGame(4, 8).Should().Be(739785);
        }

        [Fact]
        public void TestPart2Sample()
        {
            Day21.PlayDiracGame(4, 8).Should().Be(444356092776315);
        }

        [Fact]
        public void TestPart2()
        {
            Day21 day = new();
            var expectedSolution = 116741133558209L;
            day.ExecutePart2().Should().Be(expectedSolution);

        }
    }
}