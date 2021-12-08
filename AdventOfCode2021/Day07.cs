namespace AdventOfCode2021
{
    public class Day07 : IDay<long, long>
    {
        private readonly int[] _values;

        public Day07()
        {
            _values = File
                .ReadAllText(Path.Combine("Inputs", "input07.txt"))
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        public long ExecutePart1()
        {
            return GetMinFuelToAlign(_values);
        }

        public long ExecutePart2()
        {
            return GetFuelToMoveIncreasingCostV2(_values);
        }

        public static long GetMinFuelToAlign(int[] values)
        {
            var median = Median(values);
            return values.Select(v => Math.Abs(v - median)).Sum();
        }

        public static int Median(int[] values)
        {
            var valuesOrdered = values.OrderBy(x => x).ToArray();
            var middleIndex = (int)(valuesOrdered.Length / 2);
            return valuesOrdered[middleIndex];
        }

        public static long GetFuelToMoveIncreasing(int start, int end)
        {
            long totalFuel = 0;
            for (int i = 1; i <= Math.Abs(start - end); i++)
            {
                totalFuel += i;
            }

            return totalFuel;
        }

        public static long GetFuelToMoveIncreasingGauss(int start, int end)
        {
            var n = Math.Abs(start - end);
            return n * (n + 1) / 2;
        }

        public static long GetFuelToMoveIncreasingCost(int[] values)
        {
            long minTotalFuel = long.MaxValue;
            int maxValue = values.Max();
            int length = values.Length;

            for (int i = 0; i < maxValue; i++)
            {
                long currentCost = 0;
                int j = 0;
                while (currentCost < minTotalFuel && j < length)
                {
                    currentCost += GetFuelToMoveIncreasing(i, values[j]);
                    j++;
                }

                if (currentCost < minTotalFuel)
                {
                    minTotalFuel = currentCost;
                }
            }

            return minTotalFuel;
        }

        public static long GetFuelToMoveIncreasingCostV2(int[] values)
        {
            var valuesOrdered = values.OrderBy(v => v).ToArray();
            long minTotalFuel = long.MaxValue;
            int maxValue = values.Max();
            int length = values.Length;

            for (int i = 0; i <= maxValue; i++)
            {

                long currentCost = 0;
                int j = 0;
                while (currentCost < minTotalFuel && j < length)
                {
                    currentCost += GetFuelToMoveIncreasingGauss(i, valuesOrdered[j]);
                    j++;
                }

                if (currentCost < minTotalFuel)
                {
                    minTotalFuel = currentCost;
                }

            }

            return minTotalFuel;
        }
    }
}