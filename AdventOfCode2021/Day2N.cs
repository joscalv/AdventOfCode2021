namespace AdventOfCode2021
{
    public class Day2N : IDay<long, long>
    {
        private readonly int[] _values;

        public Day2N()
        {
            _values = File
                .ReadAllText(Path.Combine("Inputs", "input1N.txt"))
                .Split('\n')
                .Select(int.Parse)
                .ToArray();
        }

        public long ExecutePart1()
        {
            return 0;
        }

        public long ExecutePart2()
        {
            return 0;
        }
    }
}