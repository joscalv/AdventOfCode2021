using AdventOfCode2021;
using AdventOfCode2021.Day4;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTest
{
    public class Day04Test
    {
        private readonly Day04 _day04 = new Day04();

        [Fact]
        public void TestPart1()
        {
            var expectedSolution = 28082;
            var problem1Result = _day04.ExecutePart1();

            problem1Result.Should().Be(expectedSolution);

        }

        [Fact]
        public void TestPart2()
        {
            var expectedSolution = 8224;
            var problem1Result = _day04.ExecutePart2();

            problem1Result.Should().Be(expectedSolution);
        }

        [Fact]
        public void BingoCardTest()
        {
            int[] values = new int[] { 7, 4, 9, 5, 11, 17, 23, 2, 0, 14, 21, 24, 10, 16, 13, 6, 15, 25, 12, 22, 18, 20, 8, 19, 3, 26, 1 };
            int[][] cardValues = new int[][] {
                new int[] {14, 21, 17, 24 , 4},
                new int[] {10 ,16 ,15  ,9 ,19},
                new int[] { 18,  8 ,23 ,26 ,20},
                new int[] {22, 11, 13 , 6,  5},
                new int[] { 2 , 0, 12  ,3 , 7 }
            };

            var bingoCard = new BingoCard(cardValues);

            for (int i = 0; i <= 10; i++)
            {
                bingoCard.Play(values[i]);
                bingoCard.IsCompleted.Should().BeFalse();
            }
            bingoCard.Play(values[11]);
            bingoCard.IsCompleted.Should().BeTrue();
            bingoCard.SumUnmarked.Should().Be(188);
            bingoCard.LastValue.Should().Be(24);
        }

        [Fact]
        public void BingoCardTestColumnWin()
        {
            int[][] cardValues = new int[][] {
                new int[] {14, 21, 17, 24 , 4},
                new int[] {10 ,16 ,15  ,9 ,19},
                new int[] { 18,  8 ,23 ,26 ,20},
                new int[] {22, 11, 13 , 6,  5},
                new int[] { 2 , 0, 12  ,3 , 7 }
            };

            var bingoCard = new BingoCard(cardValues);
            var expectedUnmarkedSum = bingoCard.SumUnmarked - 14 - 10 - 18 - 22 - 2;

            bingoCard.Play(14);
            bingoCard.IsCompleted.Should().BeFalse();
            bingoCard.Play(10);
            bingoCard.IsCompleted.Should().BeFalse();
            bingoCard.Play(18);
            bingoCard.IsCompleted.Should().BeFalse();
            bingoCard.Play(22);
            bingoCard.IsCompleted.Should().BeFalse();
            bingoCard.Play(2);
            bingoCard.IsCompleted.Should().BeTrue();
            bingoCard.LastValue.Should().Be(2);
            bingoCard.SumUnmarked.Should().Be(expectedUnmarkedSum);
        }
    }
}