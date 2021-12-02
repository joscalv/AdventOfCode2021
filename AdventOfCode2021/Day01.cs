
namespace AdventOfCode2021
{
    public class Day01 : IDay<int, int>
    {

        private int[] _values;

        public Day01()
        {
            _values = File
                .ReadAllLines(Path.Combine("Inputs", "input01.txt"))
                .Select(int.Parse)
                .ToArray();
        }
        public int ExecutePart1()
        {
            var increases = 0;
            for (int i = 1; i < _values.Length; i++)
            {
                if (_values[i] > _values[i - 1])
                {
                    increases++;
                }
            }
            return increases;
        }

        public int ExecutePart2()
        {
            var increases = 0;
            for (int i = 3; i < _values.Length; i = i + 1)
            {
                if (Day01.SumWindow(_values, i) > Day01.SumWindow(_values, i - 1))
                {
                    increases++;
                }
            }
            return increases;
        }

        private static int SumWindow(int[] values, int index)
        {
            return values[index] + values[index - 1] + values[index - 2];
        }
    }
}