using AdventOfCode2021;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTest
{
    public class Day02Test
    {
       private readonly Day02 _day02 = new Day02();

        [Fact]
        public void TestPart1()
        {
            var expectedSolution = 2070300;
            var problem1Result = _day02.ExecutePart1();

            problem1Result.Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart2()
        {
            var expectedSolution = 2078985210;

            var problem1Result = _day02.ExecutePart2();

            problem1Result.Should().Be(expectedSolution);
        }
    }
}