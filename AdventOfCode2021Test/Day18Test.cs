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

            var input = @"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]".ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var result = Day18.ExecutePart1(input).Item2;//.Should().Be("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]");
            Day18.ExecutePart1(input).Item1.Should().Be(4140);



        }

        [Fact]
        public void TestPart1SampleSteps()
        {

            var l1 = "[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]";
            var l2 = "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]";
            var l3 = "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]";
            var l4 = "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]";
            var l5 = "[7,[5,[[3,8],[1,4]]]]";
            var l6 = "[[2,[2,2]],[8,[8,1]]]";
            var l7 = "[2,9]";
            var l8 = "[1,[[[9,3],9],[[9,0],[0,7]]]]";
            var l9 = "[[[5,[7,4]],7],1]";
            var l10 = "[[[[4,2],2],6],[8,7]]";

            var sum = Day18.Sum(l1.ToInteger(), l2.ToInteger());
            //sum.ConvertToString().Should().Be("[[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]");

            //var step1 = Day18.ReduceStep(sum).value;
            //step1.ConvertToString().Should().Be("[[[[4,0],[5,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]");
            //var step2 = ExecuteStepAndCheck(step1, "[[[[4,0],[5,4]],[[0,[7,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]");
            //var step3 = ExecuteStepAndCheck(step2, "[[[[4,0],[5,4]],[[7,0],[15,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]");
            //var step4 = ExecuteStepAndCheck(step3, "[[[[4,0],[5,4]],[[7,0],[[7,8],5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]");
            //var step5 = ExecuteStepAndCheck(step4, "[[[[4,0],[5,4]],[[7,7],[0,13]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]");
            //var step6 = ExecuteStepAndCheck(step5, "[[[[4,0],[5,4]],[[7,7],[0,[6,7]]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]");
            //var step7 = ExecuteStepAndCheck(step6, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[14,[[[3,7],[4,3]],[[6,3],[8,8]]]]]");
            //var step8 = ExecuteStepAndCheck(step7, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,7],[[[3,7],[4,3]],[[6,3],[8,8]]]]]");
            //var step9 = ExecuteStepAndCheck(step8, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,10],[[0,[11,3]],[[6,3],[8,8]]]]]");
            //var step10 = ExecuteStepAndCheck(step9, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,[5,5]],[[0,[11,3]],[[6,3],[8,8]]]]]");
            //var step11 = ExecuteStepAndCheck(step10, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,[5,5]],[[0,[[5,6],3]],[[6,3],[8,8]]]]]");//**duda


                          //var step11 = ExecuteStepAndCheck(step10, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,[5,5]],[[11,0],[[9,3],[8,8]]]]]");//**duda
            //var step12 = ExecuteStepAndCheck(step11, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,[5,5]],[[[5,6],0],[[9,3],[8,8]]]]]");
            //var step13 = ExecuteStepAndCheck(step12, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,[5,10]],[[0,6],[[9,3],[8,8]]]]]");
            //var step14 = ExecuteStepAndCheck(step13, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,[5,[5,5]]],[[0,6],[[9,3],[8,8]]]]]");
            //var step15 = ExecuteStepAndCheck(step14, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,[10,0]],[[5,6],[[9,3],[8,8]]]]]");
            //var step16 = ExecuteStepAndCheck(step15, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[7,[[5,5],0]],[[5,6],[[9,3],[8,8]]]]]");
            //var step17 = ExecuteStepAndCheck(step16, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[12,[0,5]],[[5,6],[[9,3],[8,8]]]]]");
            //var step18 = ExecuteStepAndCheck(step17, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[0,5]],[[5,6],[[9,3],[8,8]]]]]");
            //var step19 = ExecuteStepAndCheck(step18, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[0,5]],[[5,15],[0,[11,8]]]]]");
            //var step20 = ExecuteStepAndCheck(step19, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[0,5]],[[5,[7,8]],[0,[11,8]]]]]");
            //var step21 = ExecuteStepAndCheck(step20, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[0,5]],[[12,0],[8,[11,8]]]]]");
            //var step22 = ExecuteStepAndCheck(step21, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[0,5]],[[[6,6],0],[8,[11,8]]]]]");
            //var step23 = ExecuteStepAndCheck(step22, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[0,11]],[[0,6],[8,[11,8]]]]]");
            //var step24 = ExecuteStepAndCheck(step23, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[0,[5,6]]],[[0,6],[8,[11,8]]]]]");
            //var step25 = ExecuteStepAndCheck(step24, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[5,0]],[[6,6],[8,[11,8]]]]]");
            //var step26 = ExecuteStepAndCheck(step25, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[5,0]],[[6,6],[19,0]]]]");
            //var step27 = ExecuteStepAndCheck(step26, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[5,0]],[[6,6],[[9,10],0]]]]");
            //var step28 = ExecuteStepAndCheck(step27, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[5,0]],[[6,15],[0,10]]]]");
            //var step29 = ExecuteStepAndCheck(step28, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[[6,6],[5,0]],[[6,[7,8]],[0,10]]]]");



            Day18.Reduce(sum).ConvertToString().Should().Be("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]");


            //l1Result.ConvertToString().Should().Be("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]");
            //var step2 = Day18.ReduceStep(l1Result);
            //var step3 = Day18.ReduceStep(step2.value);
            //var step4 = Day18.ReduceStep(step3.value);
            //var r1 = Day18.Reduce(Day18.Sum(l1.ToInteger(), l2.ToInteger())).ConvertToString();
            //r1.Should().Be("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]");



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