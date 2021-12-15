namespace AdventOfCode2021
{
    public class Day11 : IDay<long, long>
    {
        private const int EnergyLevelToFlash = 10;
        private readonly int[][] _values;

        public Day11()
        {
            _values = File
                .ReadAllText(Path.Combine("Inputs", "input11.txt")).ParseInputDay11();
        }

        public long ExecutePart1()
        {
            return RunSteps(_values).totalFlases;
        }

        public long ExecutePart2()
        {
            return RunSteps(_values, -1, true).steps;
        }

        public static (int totalFlases, int steps) RunSteps(int[][] energyLevels, int maxSteps = 100, bool untilSync = false)
        {
            var totalFlashes = 0;
            var step = 0;
            var synchronized = false;
            do
            {
                step++;

                for (int y = 0; y < energyLevels.Length; y++)
                {
                    for (int x = 0; x < energyLevels[y].Length; x++)
                    {
                        energyLevels[y][x] += 1;
                        if (HasFlashed(energyLevels[y][x]))
                        {
                            IncreaseAdjacentEnergy(energyLevels, x, y);
                        }
                    }
                }

                int flashesInStep = 0;
                for (int y = 0; y < energyLevels.Length; y++)
                {
                    for (int x = 0; x < energyLevels[y].Length; x++)
                    {

                        if (energyLevels[y][x] > 9)
                        {
                            energyLevels[y][x] = 0;
                            totalFlashes++;
                            flashesInStep++;
                        }
                    }
                }
                synchronized = flashesInStep == energyLevels.Length * energyLevels[0].Length;

            } while (step < maxSteps || (untilSync && !synchronized));

            return (totalFlashes, step);
        }

        private static void IncreaseAdjacentEnergy(int[][] energyLevels, int x, int y)
        {
            energyLevels.ForEachAdjacentWithDiagonal(x, y,
                (int value, int innerX, int innerY) =>
                {
                    energyLevels[innerY][innerX] += 1;
                    if (HasFlashed(energyLevels[innerY][innerX]))
                    {
                        IncreaseAdjacentEnergy(energyLevels, innerX, innerY);
                    }
                });
        }

        private static bool HasFlashed(int energyLevel) => energyLevel == EnergyLevelToFlash;
    }

    public static class Day11Extensions
    {
        public static int[][] ParseInputDay11(this string input)
        {
            return input
                .ReplaceLineEndings()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.ToCharArray().Select(c => c - '0').ToArray())
                .ToArray();
        }

      
    }
}