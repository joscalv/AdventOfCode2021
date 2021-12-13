using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;

namespace AdventOfCode2021
{
    public class Day13 : IDay<long, long>
    {
        private readonly (int x, int y)[] _coordinates;
        private (char direction, int value)[] _instructions;

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

        public static long ExecutePart2((int x, int y)[] coordinates, (char direction, int value)[] instructions, bool print = false)
        {
            var resultCoordinates = Fold(coordinates, instructions);
            var maxX = resultCoordinates.Select(c => c.x).Max();
            var maxY = resultCoordinates.Select(c => c.y).Max();
            var coordinatesOrdered = resultCoordinates.ToHashSet();

            if (print)
            {

                for (int y = 0; y <= maxY; y++)
                {
                    for (int x = 0; x <= maxX; x++)
                    {
                        if (coordinatesOrdered.Contains((x, y)))
                        {
                            Console.Write('#');
                        }
                        else
                        {
                            Console.Write('.');
                        }
                    }

                    Console.WriteLine();
                }
            }

            return resultCoordinates.Length;
        }

        public static (int x, int y)[] Fold((int x, int y)[] coordinates, (char direction, int value)[] instructions)
        {
            var currentCoordinateS = coordinates;
            foreach (var instruction in instructions)
            {
                currentCoordinateS = Fold(currentCoordinateS, instruction);
            }

            return currentCoordinateS;
        }

        public static (int x, int y)[] Fold((int x, int y)[] coordinates, (char direction, int value) instruction)
        {
            return coordinates
                .Select(coordinate =>
                {
                    if (instruction.direction == 'y' )
                    {
                        return coordinate.y > instruction.value
                            ? (coordinate.x, instruction.value - (coordinate.y - instruction.value))
                            : coordinate;
                    }
                    else 
                    {
                        return coordinate.x > instruction.value
                            ? (instruction.value - (coordinate.x - instruction.value), coordinate.y)
                            : coordinate;
                    }

                }).Distinct().ToArray();
        }

        

    }


    public static class Day13Extensions
    {
        public static ((int x, int y)[] coordinates, (char direction, int value)[] instructions) ParseInput(string[] lines)
        {
            var coordinates = lines

                .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("fold along "))
                .Select(line => line.Split(','))
                .Select(pair => (int.Parse(pair[0]), int.Parse(pair[1])))
                .ToArray();

            var instructions = lines
                .Where(line => !string.IsNullOrWhiteSpace(line) && line.StartsWith("fold along "))
                .Select(line => (line[line.IndexOf('=') - 1], int.Parse(line.Substring(line.IndexOf('=') + 1))))
                .ToArray();

            return (coordinates, instructions);
        }
    }
}