using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day09Test
    {

        [Fact]
        public void TestPart1()
        {
            Day09 day = new();
            var expectedSolution = 478;
            day.ExecutePart1().Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart1Sample()
        {
            var input = @"21999432103987894921985678989287678967899899965678".ToCharArray().Select(c => c - '0').ToArray();
            var lavaField = new LavaField(input, 10);
            lavaField.GetLowPointValues().Should().BeEquivalentTo(new[] { 1, 0, 5, 5 });

        }
        [Fact]
        public void TestPart2()
        {
            Day09 day = new();
            var expectedSolution = 1327014;
            day.ExecutePart2().Should().Be(expectedSolution);

        }
        
        [Fact]
        public void TestPart2Sample()
        {

            var input = @"21999432103987894921985678989287678967899899965678".ToCharArray().Select(c => c - '0').ToArray();
            Day09.ExecutePart2(input, 10).Should().Be(1134);

        }
    }
}