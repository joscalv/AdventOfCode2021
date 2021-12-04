
using AdventOfCode2021.Day4;

namespace AdventOfCode2021
{

    public class Day04 : IDay<int, int>
    {
        private int[] _playValues;

        private readonly List<int[][]> _boards;

        public Day04()
        {
            var path = Path.Combine("Inputs", "input03.txt");

            var lines = File.ReadAllLines(Path.Combine("Inputs", "input04.txt"));

            _playValues = lines[0].Split(',').Select(int.Parse).ToArray();

            _boards = lines
                .Skip(1)
                .Where(l => l != String.Empty)
                .Select((l, ix) => (ix, l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()))
                .GroupBy(l => (int)(l.ix / 5))
                .Select(g => g.Select(i => i.Item2).ToArray())
                .ToList();
        }

        public int ExecutePart1()
        {
            var bingoCards = _boards.Select(x => new BingoCard(x)).ToList();
            foreach (var value in _playValues)
            {
                foreach (var card in bingoCards)
                {
                    var isCompleted = card.Play(value);
                    if (isCompleted)
                    {
                        return card.SumUnmarked * card.LastValue;
                    }
                }
            }
            return -1;
        }

        public int ExecutePart2()
        {
            var bingoCards = _boards.Select(x => new BingoCard(x)).ToList();
            BingoCard? lastWinner = null;
            foreach (var value in _playValues)
            {
                foreach (var card in bingoCards)
                {
                    if (!card.IsCompleted)
                    {
                        var isCompleted = card.Play(value);
                        if (isCompleted)
                        {
                            lastWinner = card;
                        }
                    }
                }
            }
            return lastWinner != null ? lastWinner.SumUnmarked * lastWinner.LastValue : -1;
        }

    }
}