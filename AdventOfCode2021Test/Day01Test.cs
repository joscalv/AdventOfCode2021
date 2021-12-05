using AdventOfCode2021;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021Test
{
    public class Day01Test
    {
        [Fact]
        public void Day1TestTest1()
        {
            var expectedSolution = 1121;
            var day01 = new Day01();
            var problem1Result = day01.ExecutePart1();

            problem1Result.Should().Be(expectedSolution);

        }

        [Fact]
        public void Day1TestTest2()
        {
            var expectedSolution = 1065;
            var day01 = new Day01();
            var problem1Result = day01.ExecutePart2();

            problem1Result.Should().Be(expectedSolution);
        }
    }
}