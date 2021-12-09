namespace AdventOfCode2021
{
    public class Day09 : IDay<long, long>
    {
        private readonly int[] _values;
        private readonly int _width;

        public Day09()
        {
            int width = -1;
            _values = File
               .ReadAllText(Path.Combine("Inputs", "input09.txt"))
               .Split('\n')
               .SelectMany(l =>
               {
                   width = width < 0 ? l.Length : width;
                   return l.ToCharArray().Select(c => c - '0').ToArray();
               })
               .ToArray();
            _width = width;
        }

        public long ExecutePart1()
        {
            var lavaField = new LavaField(_values, _width);
            var riskLevels = lavaField.GetLowPointValues();
            return riskLevels.Select(v => v + 1).Sum();
        }

        public long ExecutePart2()
        {
            return ExecutePart2(_values, _width);
        }

        public static long ExecutePart2(int[] values, int width)
        {
            var lavaField = new LavaField(values, width);
            var basins = lavaField.GetBasins();
            long result = 1;
            basins.Select(b => b.Count).OrderByDescending(v => v).Take(3).ToList().ForEach(v => result *= v);

            return result;
        }
    }

    public class LavaField
    {
        private readonly int[] _heights;
        private readonly int _width;
        public LavaField(int[] heights, int width)
        {
            _heights = heights;
            _width = width;
        }


        public List<(int center, int height)> GetBasinsPoints()
        {
            var riskPoints = new List<(int center, int height)>();
            int lastInRow = _width - 1;
            for (int i = 0; i < _heights.Length; i++)
            {
                var currentValue = _heights[i];
                if ((i % _width == 0 || currentValue < GetValue(i - 1))
                    && (i % _width == lastInRow || currentValue < GetValue(i + 1))
                    && currentValue < GetValue(i - _width)
                    && currentValue < GetValue(i + _width))
                {
                    riskPoints.Add((i, currentValue));
                }
            }
            return riskPoints;
        }

        public List<List<int>> GetBasins()
        {
            var basinsByCenter = GetBasinsPoints()
                .ToDictionary(
                    b => b.center,
                    b => new List<int>() { b.height });

            HashSet<int> isInBasin = new();

            foreach (var basin in basinsByCenter.Keys)
            {
                isInBasin.Add(basin);
                Queue<int> pointsToCheck = new Queue<int>();
                pointsToCheck.Enqueue(basin);
                while (pointsToCheck.Any())
                {
                    var currentPoint = pointsToCheck.Dequeue();
                    foreach (var adjacent in GetAdjacents(currentPoint))
                    {
                        if (!isInBasin.Contains(adjacent) && _heights[adjacent] != 9)
                        {
                            isInBasin.Add(adjacent);
                            pointsToCheck.Enqueue(adjacent);
                            basinsByCenter[basin].Add(_heights[adjacent]);
                        }
                    }
                }
            }

            return basinsByCenter.Select(kvp => kvp.Value).ToList();
        }

        public List<int> GetLowPointValues()
        {
            return GetBasinsPoints().Select(b => b.height).ToList();
        }

        private int GetValue(int index)
        {
            return index >= 0 && index < _heights.Length ? _heights[index] : int.MaxValue;
        }

        private IEnumerable<int> GetAdjacents(int i)
        {
            int lastInRow = _width - 1;

            if (i % _width != 0)
            {
                yield return i - 1;
            }


            if (i % _width != lastInRow)
            {
                yield return i + 1;
            }

            if (i >= _width)
            {
                yield return i - _width;
            }

            if (i + _width < _heights.Length)
            {
                yield return i + _width;
            }
        }
    }

}