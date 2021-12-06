namespace AdventOfCode2021
{
    public class Day06 : IDay<long, long>
    {
        private readonly List<int> _values;

        public Day06()
        {
            _values = File
                .ReadAllText(Path.Combine("Inputs", "input06.txt"))
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }

        public long ExecutePart1()
        {
            return CalculateFishNumber(_values, 80);
        }

        public long ExecutePart2()
        {
            return CalculateFishNumber(_values, 256);
        }

        public static List<int> CalculateArray(List<int> values, int days)
        {
            var timers = values.ToList();
            int newFishes = 0;
            for (int dayCounter = 0; dayCounter < days; dayCounter++)
            {
                newFishes = 0;
                for (int i = 0; i < timers.Count; i++)
                {
                    if (timers[i] == 0)
                    {
                        newFishes++;
                    }
                    timers[i] = timers[i] == 0 ? 6 : timers[i] - 1;
                }

                for (int i = 0; i < newFishes; i++)
                {
                    timers.Add(8);
                }
            }


            return timers;
        }

        public static long CalculateFishNumber(List<int> values, int days)
        {
            long[] counters = new long[9];
            foreach (var value in values)
            {
                counters[value]++;
            }

            for (int dayCounter = 0; dayCounter < days; dayCounter++)
            {
                var newFish = counters[0];
                for (int i = 0; i < counters.Length - 1; i++)
                {
                    counters[i] = counters[i + 1];
                }

                counters[6] += newFish;
                counters[8] = newFish;
            }


            return counters.Sum();
        }
    }
}