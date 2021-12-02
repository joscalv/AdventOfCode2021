
namespace AdventOfCode2021
{
    public class Day02 : IDay<int, int>
    {

        struct PositionPart2
        {
            public PositionPart2()
            {
                X = 0;
                Y = 0;
                Aim = 0;
            }
            public PositionPart2(int x, int y, int aim)
            {
                X = x;
                Y = y;
                Aim = aim;
            }

            internal int X;
            internal int Y;
            internal int Aim;
        }

        private (string command, int units)[] _values;

        public Day02()
        {
            _values = File
                .ReadAllLines(Path.Combine("Inputs", "input02.txt"))
                .Select(line =>
                {
                    var values = line.Split(' ');
                    return (values[0], int.Parse(values[1]));
                })
                .ToArray();
        }
        public int ExecutePart1()
        {
            (int X, int Y) currentPosition = (0, 0);
            foreach (var command in _values)
            {
                currentPosition = MovePart1(currentPosition, command);
            }
            return currentPosition.X * currentPosition.Y;
        }

        public int ExecutePart2()
        {
            PositionPart2 currentPosition = new PositionPart2();
            foreach (var command in _values)
            {
                currentPosition = MovePart2(currentPosition, command);
            }
            return currentPosition.X * currentPosition.Y;
        }

        private static (int X, int Y) MovePart1((int X, int Y) currentPosition, (string command, int units) movement) =>
            movement.command switch
            {
                "forward" => (currentPosition.X + movement.units, currentPosition.Y),
                "up" => (currentPosition.X, currentPosition.Y - movement.units),
                "down" => (currentPosition.X, currentPosition.Y + movement.units),
                _ => currentPosition,
            };

        private static PositionPart2 MovePart2(PositionPart2 currentPosition, (string command, int units) movement) =>
            movement.command switch
            {
                "forward" => new PositionPart2(currentPosition.X + movement.units, currentPosition.Y + currentPosition.Aim * movement.units, currentPosition.Aim),
                "up" => new PositionPart2(currentPosition.X, currentPosition.Y, currentPosition.Aim - movement.units),
                "down" => new PositionPart2(currentPosition.X, currentPosition.Y, currentPosition.Aim + movement.units),
                _ => currentPosition,
            };



    }
}