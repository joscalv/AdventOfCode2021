namespace AdventOfCode2021
{
    public class Day13 : IDay<long, long>
    {
        public record FoldCoordinate(int X, int Y);
        public record FoldInstruction(char Direction, int Value);

        private readonly FoldCoordinate[] _coordinates;
        private readonly FoldInstruction[] _instructions;

        public Day13()
        {
            var lines = File
                .ReadAllText(Path.Combine("Inputs", "input13.txt"))
                .ReplaceLineEndings()
                .Split(Environment.NewLine)
                .ToArray();

            var input = Day13Extensions.ParseInput(lines);
            _coordinates = input.coordinates;
            _instructions = input.instructions;

        }

        public long ExecutePart1()
        {
            var result = Fold(_coordinates, _instructions.Take(1).ToArray());
            return result.Length;
        }

        public long ExecutePart2()
        {
            return ExecutePart2(_coordinates, _instructions, false);
        }

        public long ExecutePart2AndPrint()
        {
            return ExecutePart2(_coordinates, _instructions, true);
        }

        public static long ExecutePart2(FoldCoordinate[] coordinates, FoldInstruction[] instructions, bool print = false)
        {
            var resultCoordinates = Fold(coordinates, instructions);
            var maxX = resultCoordinates.Select(c => c.X).Max();
            var maxY = resultCoordinates.Select(c => c.Y).Max();
            var coordinatesOrdered = resultCoordinates.ToHashSet();

            if (print)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    for (int x = 0; x <= maxX; x++)
                    {
                        Console.WriteLine(coordinatesOrdered.Contains(new FoldCoordinate(x, y)) ? '#' : '.');
                    }
                    Console.WriteLine();
                }
            }

            return resultCoordinates.Length;
        }

        public static FoldCoordinate[] Fold(FoldCoordinate[] coordinates, FoldInstruction[] instructions)
        {
            var currentCoordinateS = coordinates;
            foreach (var instruction in instructions)
            {
                currentCoordinateS = Fold(currentCoordinateS, instruction);
            }

            return currentCoordinateS;
        }

        public static FoldCoordinate[] Fold(FoldCoordinate[] coordinates, FoldInstruction instruction)
        {
            return coordinates
                .Select(coordinate => coordinate with
                {
                    Y = instruction.Direction == 'y' && coordinate.Y > instruction.Value
                        ? 2 * instruction.Value - coordinate.Y
                        : coordinate.Y,
                    X = instruction.Direction == 'x' && coordinate.X > instruction.Value
                        ? 2 * instruction.Value - coordinate.X
                        : coordinate.X,
                }).Distinct().ToArray();
        }

        public static class Day13Extensions
        {
            public static (FoldCoordinate[] coordinates, FoldInstruction[] instructions) ParseInput(string[] lines)
            {
                var coordinates = lines
                    .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("fold along "))
                    .Select(line => line.Split(','))
                    .Select(pair => new FoldCoordinate(int.Parse(pair[0]), int.Parse(pair[1])))
                    .ToArray();

                var instructions = lines
                    .Where(line => line.StartsWith("fold along "))
                    .Select(line => new FoldInstruction(line[line.IndexOf('=') - 1], int.Parse(line.Substring(line.IndexOf('=') + 1))))
                    .ToArray();

                return (coordinates, instructions);
            }
        }

    }



}