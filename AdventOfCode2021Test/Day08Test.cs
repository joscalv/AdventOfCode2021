using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day08Test
    {

        [Fact]
        public void TestPart1()
        {
            Day08 day = new();
            var expectedSolution = 261;
            day.ExecutePart1().Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart1Sample()
        {

            var inputStr = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

            var input = Day08Extensions.ParseInputDay08(inputStr);
            //Day08 day = new Day08();
            //var expectedSolution = 0;
            Day08.ExecutePart1(input).Should().Be(26);

        }
        [Fact]

        public void TestPart2Sample2()
        {

            var inputStr = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

            var input = Day08Extensions.ParseInputDay08(inputStr);

            foreach (var entry08 in input)
            {
                Day08Extensions.IdentifyNumbers(entry08.Inputs);
            }
            Day08.ExecutePart2(input).Should().Be(61229);

        }

        [Fact]
        public void TestPart2Sample1()
        {

            var inputStr = @"acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab".Split(' ');

            var numbersIdentified = Day08Extensions.IdentifyNumbers(inputStr);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("acedgfb"), 8);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("cdfbe"), 5);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("gcdfa"), 2);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("fbcad"), 3);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("dab"), 7);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("cefabd"), 9);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("cdfgeb"), 6);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("eafb"), 4);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("cagedb"), 0);
            numbersIdentified.Should().Contain(Day08Extensions.OrderSegment("ab"), 1);

            var digits = "cdfeb fcadb cdfeb cdbaf".Split(' ').ToArray();
            Day08Extensions.FixDisplayView(numbersIdentified, digits).Should().Be(5353);
        }
        [Fact]
        public void TestPart2()
        {
            Day08 day = new();
            var expectedSolution = 987553;
            day.ExecutePart2().Should().Be(expectedSolution);

        }
    }
}