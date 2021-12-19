using System;
using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day18Test
    {

        [Fact]
        public void TestPart1()
        {
            Day18 day = new();
            var expectedSolution = 3884;
            day.ExecutePart1().Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart1Sample()
        {

            var input = @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]".ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Day18.ExecutePart1(input).Item1.Should().Be(4140);
        }

        public List<int> ExecuteStepAndCheck(List<int> sum1, string expected)
        {
            var result = Day18.ReduceStep(sum1).value;
            result.ConvertToString().Should().Be(expected);
            return result;
        }


        [Fact]
        public void TestPart1SampleSteps2()
        {

            var input = @"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]".ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

            var r1 = Day18.Reduce(Day18.Sum(input[0].ToInteger(), input[1].ToInteger()));
            var r2 = Day18.Reduce(Day18.Sum(r1, input[2].ToInteger()));
            var r3 = Day18.Reduce(Day18.Sum(r2, input[3].ToInteger()));
            Day18.Reduce(Day18.Sum(r3, input[4].ToInteger())).ConvertToString().Should().Be("[[[[3,0],[5,3]],[4,4]],[5,5]]");
        }

        [Fact]
        public void TestPart2()
        {
            Day18 day = new();
            var expectedSolution = 4595;
            day.ExecutePart2().Should().Be(expectedSolution);
        }

        [Fact]
        public void ReduceTest()
        {
            Day18.ReduceStep("[[[[[9,8],1],2],3],4]".ToInteger()).value.ConvertToString().Should().Be("[[[[0,9],2],3],4]");
            Day18.ReduceStep("[7,[6,[5,[4,[3,2]]]]]".ToInteger()).value.ConvertToString().Should().Be("[7,[6,[5,[7,0]]]]");
            Day18.ReduceStep("[[6,[5,[4,[3,2]]]],1]".ToInteger()).value.ConvertToString().Should().Be("[[6,[5,[7,0]]],3]");
            Day18.ReduceStep("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]".ToInteger()).value.ConvertToString().Should().Be("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            Day18.ReduceStep("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]".ToInteger()).value.ConvertToString().Should().Be("[[3,[2,[8,0]]],[9,[5,[7,0]]]]");
        }

        [Fact]
        public void ReduceTest2()
        {
            Day18.ReduceStep("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]".ToInteger()).value.ConvertToString().Should().Be("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]");
            Day18.ReduceStep("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]".ToInteger()).value.ConvertToString().Should().Be("[[[[0,7],4],[15,[0,13]]],[1,1]]");
            Day18.ReduceStep("[[[[0,7],4],[15,[0,13]]],[1,1]]".ToInteger()).value.ConvertToString().Should().Be("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]");
            Day18.ReduceStep("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]".ToInteger()).value.ConvertToString().Should().Be("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]");
            Day18.ReduceStep("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]".ToInteger()).value.ConvertToString().Should().Be("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
        }

        [Fact]
        public void ReduceTest3()
        {
            Day18.Reduce("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]".ToInteger());//.ConvertToString().Should().Be("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
        }

        [Fact]
        public void GetMagnitudeTest()
        {
            Day18.GetMagnitude("[[1,2],[[3,4],5]]".ToInteger()).Should().Be(143);
            Day18.GetMagnitude("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]".ToInteger()).Should().Be(1384);
            Day18.GetMagnitude("[[[[1,1],[2,2]],[3,3]],[4,4]]".ToInteger()).Should().Be(445);
            Day18.GetMagnitude("[[[[3,0],[5,3]],[4,4]],[5,5]]".ToInteger()).Should().Be(791);
            Day18.GetMagnitude("[[[[5,0],[7,4]],[5,5]],[6,6]]".ToInteger()).Should().Be(1137);
            Day18.GetMagnitude("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]".ToInteger()).Should().Be(3488);
        }

        [Fact]
        public void SumTest()
        {
            Day18.Sum("[[[[4,3],4],4],[7,[[8,4],9]]]".ToInteger(), "[1,1]".ToInteger()).ConvertToString().Should().Be("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");
        }
    }
}