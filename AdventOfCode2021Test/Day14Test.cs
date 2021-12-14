using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day14Test
    {

        private readonly string _sample1Str =
            @"NNCB
CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

        [Fact]
        public void TestPart1()
        {
            Day14 day = new();
            var expectedSolution = 2027;
            day.ExecutePart1().Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart1Sample1()
        {
            var sampleInput = Day14Extensions.Parse(_sample1Str);

            Day14.ApplyRules(sampleInput.rules, sampleInput.template, 1)
                .Should()
                .Be((int)GetDifference("NCNBCHB"));

            Day14.ApplyRules(sampleInput.rules, sampleInput.template, 2)
                .Should()
                .Be((int)GetDifference("NBCCNBBBCBHCB"));

            Day14.ApplyRules(sampleInput.rules, sampleInput.template, 3)
                .Should()
                .Be((int)GetDifference("NBBBCNCCNBBNBNBBCHBHHBCHB"));

            Day14.ApplyRules(sampleInput.rules, sampleInput.template, 4)
                .Should()
                .Be((int)GetDifference("NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB"));

            var expectedSolution = 1588;
            Day14.ApplyRules(sampleInput.rules, sampleInput.template, 10).Should().Be(expectedSolution);
        }

        [Fact]
        public void TestPart2Sample1()
        {
            var sampleInput = Day14Extensions.Parse(_sample1Str);

            Day14.ApplyRulesOptimized(sampleInput.rules, sampleInput.template, 1)
                .Should()
                .Be(GetDifference("NCNBCHB"));

            Day14.ApplyRulesOptimized(sampleInput.rules, sampleInput.template, 2)
            .Should()
            .Be(GetDifference("NBCCNBBBCBHCB"));

            Day14.ApplyRulesOptimized(sampleInput.rules, sampleInput.template, 3)
                .Should()
                .Be(GetDifference("NBBBCNCCNBBNBNBBCHBHHBCHB"));

            Day14.ApplyRulesOptimized(sampleInput.rules, sampleInput.template, 4)
                .Should()
                .Be(GetDifference("NBBNBNBBCCNBCNCCNBBNBBNBBBNBBNBBCBHCBHHNHCBBCBHCB"));

            var expectedSolution = 2188189693529UL;
            Day14.ApplyRulesOptimized(sampleInput.rules, sampleInput.template, 40)
                .Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day14 day = new();
            ulong expectedSolution = 2265039461737;
            day.ExecutePart2().Should().Be(expectedSolution);

        }

        private static ulong GetDifference(string input)
        {
            var repetitions = input.ToCharArray().GroupBy(c => c).Select(g => g.Count()).ToList();
            return (ulong)(repetitions.Max() - repetitions.Min());
        }
    }
}