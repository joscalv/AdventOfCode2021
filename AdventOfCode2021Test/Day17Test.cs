using AdventOfCode2021;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode2021Test
{

    public class Day17Test
    {

        [Fact]
        public void TestPart1()
        {
            Day17 day = new();
            var expectedSolution = 12090;
            day.ExecutePart1().Should().Be(expectedSolution);

        }
        [Fact]
        public void TestPart2()
        {
            Day17 day = new();
            var expectedSolution = 5059;
            day.ExecutePart2().Should().Be(expectedSolution);

        }
        
       [Fact]
        public void TestPart1Sample1()
        {
            Day17.CheckTrajectories((20, 30), (-10, -5)).maxY.Should().Be(45);
            Day17.CheckTrajectories((20, 30), (-10, -5)).numberOfSolutions.Should().Be(112);
            

        }
        
        [Fact]
        public void TestTarget()
        {
            Day17.CheckMatchTarget(3, 3, (20, 30), (-10, -5)).Item1.Should().Be(false);
            Day17.CheckMatchTarget(17, -4, (20, 30), (-10, -5)).Item1.Should().Be(false);
            Day17.CheckMatchTarget(7,2,(20, 30), (-10, -5)).Item1.Should().Be(true);
            Day17.CheckMatchTarget(6,3,(20, 30), (-10, -5)).Item1.Should().Be(true);
            Day17.CheckMatchTarget(9,0,(20, 30), (-10, -5)).Item1.Should().Be(true);
            
            Day17.CheckMatchTarget(6,9,(20, 30), (-10, -5)).Item1.Should().Be(true);
            Day17.CheckMatchTarget(6,9,(20, 30), (-10, -5)).maxY.Should().Be(45);
            
            

        }
    }
}