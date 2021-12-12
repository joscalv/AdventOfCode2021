using System;
using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day12Test
    {

        private readonly Dictionary<string, List<string>> _inputSample1;
        private readonly Dictionary<string, List<string>> _inputSample2;
        private readonly Dictionary<string, List<string>> _inputSample3;

        public Day12Test()
        {
            _inputSample1 = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end".ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).GetPathsDay12();

            _inputSample2 = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc".ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).GetPathsDay12();


            _inputSample3 = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW".ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).GetPathsDay12();
        }

        [Fact]
        public void TestPart1()
        {
            Day12 day = new();
            var expectedSolution = 4186;
            day.ExecutePart1().Should().Be(expectedSolution);
        }

        [Fact]
        public void TestPart2()
        {
            Day12 day = new();
            var expectedSolution = 92111;
            day.ExecutePart2V2().Should().Be(expectedSolution);
        }
        
        //[Fact]
        //public void TestPart2V2()
        //{
        //    Day12 day = new();
        //    var expectedSolution = 92111;
        //    day.ExecutePart2V2().Should().Be(expectedSolution);
        //}

        [Fact]
        public void TestPart1Sample1()
        {

            Day12.CalcNumberOfPaths(_inputSample1).Should().Be(10);
        }

        [Fact]
        public void TestPart1Sample2()
        {
            Day12.CalcNumberOfPaths(_inputSample2).Should().Be(19);
        }

        [Fact]
        public void TestPart1Sample3()
        {
            Day12.CalcNumberOfPaths(_inputSample3).Should().Be(226);

        }
        [Fact]
        public void TestPart2Sample2()
        {
            Day12.CalcNumberOfPathsVisitingOneSmallTwice(_inputSample2).Should().Be(103);

        }

        [Fact]
        public void TestPart2Sample3()
        {
            Day12.CalcNumberOfPathsVisitingOneSmallTwice(_inputSample3).Should().Be(3509);

        }


        [Fact]
        public void TestPart2Sample1()
        {
            Day12.CalcNumberOfPathsVisitingOneSmallTwice(_inputSample1).Should().Be(36);

        }
    }
}