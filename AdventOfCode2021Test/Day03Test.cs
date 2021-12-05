using AdventOfCode2021;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021Test
{
    public class Day03Test
    {
       private readonly Day03 _day03 = new Day03();

        [Fact]
        public void TestPart1()
        {
            var expectedSolution = 2498354;
            var problem1Result = _day03.ExecutePart1();

            problem1Result.Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart2()
        {
            var expectedSolution = 3277956;

            var problem1Result = _day03.ExecutePart2();

            problem1Result.Should().Be(expectedSolution);
        }
    }
}