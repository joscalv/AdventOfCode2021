using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day06Test
    {

        [Fact]
        public void TestPart1()
        {
            Day06 day06 = new Day06();
            var expectedSolution = 355386;
            var problem1Result = day06.ExecutePart1();
            problem1Result.Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day06 day06 = new Day06();
            var expectedSolution = 1613415325809L;
            var problem1Result = day06.ExecutePart2();
            problem1Result.Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart1Sample()
        {
            List<int> sample = new() { 3, 4, 3, 1, 2 };
            Day06.CalculateArray(sample, 1).Should().BeEquivalentTo(new List<int>() { 2, 3, 2, 0, 1 });
            Day06.CalculateArray(sample, 2).Should().BeEquivalentTo(new List<int>() { 1, 2, 1, 6, 0, 8 });
            Day06.CalculateArray(sample, 18).Should().BeEquivalentTo(new List<int>() { 6, 0, 6, 4, 5, 6, 0, 1, 1, 2, 6, 0, 1, 1, 1, 2, 2, 3, 3, 4, 6, 7, 8, 8, 8, 8 });
            Day06.CalculateArray(sample, 80).Count.Should().Be(5934);

        }

        [Fact]
        public void TestPart2Sample()
        {
            List<int> sample = new() { 3, 4, 3, 1, 2 };
            Day06.CalculateFishNumber(sample, 80).Should().Be(5934);
            Day06.CalculateFishNumber(sample, 256).Should().Be(26984457539);
        }
    }
}