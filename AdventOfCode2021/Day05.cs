namespace AdventOfCode2021
{

    public record Point(int X, int Y);

    public record Line(Point P0, Point P1)
    {
        public Line(int x0, int y0, int x1, int y1) : this(new Point(x0, y0), new Point(x1, y1)) { }

    }

    public class Day05 : IDay<int, int>
    {


        private readonly Line[] _lines;

        public Day05()
        {
            _lines = File
                .ReadAllLines(Path.Combine("Inputs", "input05.txt"))
                .Select(Day05Extensions.ParseLine)
                .ToArray();
        }

        public int ExecutePart1()
        {
            return ExecutePart1(_lines);
        }

        public int ExecutePart1(Line[] lines)
        {
            var verticalLines = lines.Where(Day05Extensions.IsHorizontalOrVertical).ToArray();
            Dictionary<Point, int> points = new Dictionary<Point, int>();
            foreach (var line in verticalLines)
            {
                foreach (var point in line.GetLinePoints())
                {
                    if (points.ContainsKey(point))
                    {
                        points[point] += 1;
                    }
                    else
                    {
                        points.Add(point, 1);
                    }
                }
            }
            return points.Count(kv => kv.Value >= 2);
        }

        public int ExecutePart2(Line[] lines)
        {
            Dictionary<Point, int> points = new Dictionary<Point, int>();
            foreach (var line in lines)
            {
                foreach (var point in line.GetLinePoints())
                {
                    if (points.ContainsKey(point))
                    {
                        points[point] += 1;
                    }
                    else
                    {
                        points.Add(point, 1);
                    }
                }
            }
            return points.Count(kv => kv.Value >= 2);
        }


        public int ExecutePart2()
        {
            return ExecutePart2(_lines);
        }

    }

    public static class Day05Extensions
    {
        public static Line ParseLine(string value)
        {
            //941,230 -> 322,849
            var points = value
                .Split(" -> ")
                .SelectMany(pStr => pStr.Split(","))
                .Select(int.Parse)
                .ToArray();

            return new Line(new Point(points[0], points[1]), new Point(points[2], points[3]));
        }

        public static bool IsHorizontalOrVertical(this Line line) => line.P0.X == line.P1.X || line.P0.Y == line.P1.Y;

        //public static Line CreateLine(int x0, int y0, int x1, int y1) => new(new Point(x0, y0), new Point(x1, y1));


        public static IEnumerable<Point> GetLinePoints(this Line line)
        {
            yield return line.P0;

            int m = (line.P1.X - line.P0.X) == 0 ? int.MaxValue : ((line.P1.Y - line.P0.Y) / (line.P1.X - line.P0.X));

            if (m != int.MaxValue)
            {
                var (x0, x1) = line.P0.X < line.P1.X ? (line.P0.X, line.P1.X) : (line.P1.X, line.P0.X);
                for (int newX = x0 + 1; newX < x1; newX++)
                {
                    var newY = m * (newX - line.P0.X) + line.P0.Y;
                    yield return new Point(newX, newY);
                }
            }
            else
            {
                var (y0, y1) = line.P0.Y < line.P1.Y ? (line.P0.Y, line.P1.Y) : (line.P1.Y, line.P0.Y);
                for (int newY = y0 + 1; newY < y1; newY++)
                {
                    var newX = line.P0.X;
                    yield return new Point(newX, newY);
                }
            }

            yield return line.P1;
        }
    }
}