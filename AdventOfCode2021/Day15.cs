namespace AdventOfCode2021
{
    public class Day15 : IDay<int, int>
    {
        private readonly int[][] _costMatrix;

        public Day15()
        {
            _costMatrix = File
                .ReadAllText(Path.Combine("Inputs", "input15.txt"))
                .Split('\n')
                .Where(l => l != string.Empty)
                .Select(line => line.ToCharArray().Select(c => c - '0').ToArray())
                .ToArray();
        }

        public int ExecutePart1()
        {
            return FindShortestPath(_costMatrix);
        }

        public int ExecutePart2()
        {
            var expandedMatrix = ExpandMatrix(_costMatrix);

            return FindShortestPath(expandedMatrix);
        }

        public static int[][] ExpandMatrix(int[][] ìnput)
        {
            var yLength = ìnput.Length * 5;
            var xLength = ìnput[0].Length * 5;
            var y0Length = ìnput.Length;
            var x0Length = ìnput[0].Length;
            var extendedMatrix = new int[yLength][];
            for (int y = 0; y < yLength; y++)
            {
                extendedMatrix[y] = new int[xLength];
                for (int x = 0; x < xLength; x++)
                {
                    var newValue = (ìnput[y % y0Length][x % x0Length] + (y / y0Length) + (x / x0Length));
                    extendedMatrix[y][x] = newValue <= 9 ? newValue : newValue % 9;
                }
            }

            return extendedMatrix;
        }

        public static int FindShortestPath(int[][] matrix)
        {
            PriorityQueue<Point, int>
                accumulatedCost = new PriorityQueue<Point, int>();
            accumulatedCost.Enqueue(new Point(0, 0), 0);

            HashSet<(int, int)> visited = new HashSet<(int, int)> { (0, 0) };

            Point destination = new Point(matrix.Length - 1, matrix[0].Length - 1);
            Point currentPosition = new Point(0, 0);

            int cost = 0;
            while (currentPosition != destination
                   && accumulatedCost.TryDequeue(out currentPosition, out cost))
            {
                matrix.ForEachAdjacent(currentPosition.X, currentPosition.Y, (c, x, y) =>
                {
                    if (!visited.Contains((x, y)))
                    {
                        accumulatedCost.Enqueue(new Point(x, y), cost + c);
                        visited.Add((x, y));
                    }
                });
            }

            return cost;
        }
    }
}