using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day07Test
    {

        [Fact]
        public void TestPart1()
        {
            Day07 day07 = new Day07();
            var expectedSolution = 347011L;
            day07.ExecutePart1().Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day07 day07 = new Day07();
            var expectedSolution = 98363777L;
            day07.ExecutePart2().Should().Be(expectedSolution);

        }

        [Fact]
        public void MedianTest()
        {
            var values = new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            Day07.Median(values).Should().Be(2);
        }

        [Fact]
        public void GetMinFuelToAlignTest()
        {
            var values = new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            Day07.GetMinFuelToAlign(values).Should().Be(37);
        }

        [Theory]
        [InlineData(16, 5, 66)]
        [InlineData(1, 5, 10)]
        [InlineData(2, 5, 6)]
        [InlineData(0, 5, 15)]
        [InlineData(4, 5, 1)]
        [InlineData(7, 5, 3)]
        [InlineData(14, 5, 45)]
        public void GetFuelToMove(int start, int end, int expected)
        {
            Day07.GetFuelToMoveIncreasing(start, end).Should().Be(expected);
        }

        [Fact]
        public void GetMinTotalFuelTest()
        {
            var values = new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            Day07.GetFuelToMoveIncreasingCost(values).Should().Be(168);
        }
        
        [Fact]
        public void GetMinTotalFuelV2Test()
        {
            var values = new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            Day07.GetFuelToMoveIncreasingCostV2(values).Should().Be(168);
        }
    }
}