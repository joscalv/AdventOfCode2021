using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AdventOfCode2021
{
    public class Day12 : IDay<long, long>
    {
        private readonly string[] _input;

        public Day12()
        {
            _input = File
                .ReadAllLines(Path.Combine("Inputs", "input12.txt"))
                .ToArray();
        }

        public long ExecutePart1()
        {
            var paths = Day12Extensions.GetPathsDay12(_input);
            return CalcNumberOfPaths(paths);
        }

        public long ExecutePart2()
        {
            var paths = _input.GetPathsDay12();
            return CalcNumberOfPathsVisitingOneSmallTwiceV2(paths);
        }

        public long ExecutePart2V2()
        {
            var paths = _input.GetPathsDay12();
            return CalcNumberOfPathsVisitingOneSmallTwiceV2(paths);
        }

        public static long CalcNumberOfPaths(Dictionary<string, List<string>> paths)
        {
            var possiblePaths = new Stack<List<string>>();
            possiblePaths.Push(new List<string>() { "start" });
            var numberOfPaths = 0;

            while (possiblePaths.Any())
            {
                var currentPath = possiblePaths.Pop();

                if (paths.TryGetValue(currentPath.Last(), out var next))
                {
                    foreach (var nextDestination in next)
                    {
                        if (nextDestination == "end")
                        {
                            numberOfPaths++;
                        }
                        else if (nextDestination.Any(char.IsUpper) || !currentPath.Contains(nextDestination))
                        {
                            possiblePaths.Push(currentPath.Append(nextDestination).ToList());
                        }
                    }
                }
            }

            return numberOfPaths;
        }

        public static long CalcNumberOfPathsVisitingOneSmallTwice(Dictionary<string, List<string>> paths)
        {
            var possiblePaths = new Stack<List<(string, bool)>>();
            possiblePaths.Push(new List<(string, bool)>() { ("start", true) });
            var numberOfPaths = 0;

            var paths2 = paths.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(dest => (dest, dest.All(char.IsLower))).ToList());


            while (possiblePaths.Any())
            {
                var currentPath = possiblePaths.Pop();

                if (paths2.TryGetValue(currentPath.Last().Item1, out var next))
                {
                    foreach (var nextDestination in next)
                    {
                        if (nextDestination.dest == "end")
                        {
                            numberOfPaths++;
                        }
                        else if (!nextDestination.Item2 || CanBeVisited(currentPath, nextDestination))
                        {
                            possiblePaths.Push(currentPath.Append(nextDestination).ToList());
                        }
                    }
                }
            }

            return numberOfPaths;
        }


        private record Cave(string Name, bool IsSmall);
        private record CavePath(Cave[] Points, bool HasTwoEqSmall);

        public static long CalcNumberOfPathsVisitingOneSmallTwiceV2(Dictionary<string, List<string>> paths)
        {
            var possiblePaths = new Stack<CavePath>();
            possiblePaths.Push(new CavePath(new Cave[] { new Cave("start", false) }, false));


            var numberOfPaths = 0;

            var paths2 = paths.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(dest => new Cave(dest, dest.All(char.IsLower))).ToList());


            while (possiblePaths.Any())
            {
                var currentPath = possiblePaths.Pop();

                if (paths2.TryGetValue(currentPath.Points.Last().Name, out var next))
                {
                    for (int i = 0; i < next.Count; i++)
                    {
                        var nextDestination = next[i];

                        var newEqSmallCount = currentPath.HasTwoEqSmall;
                        if (nextDestination.Name == "end")
                        {
                            numberOfPaths++;
                        }
                        else if (!nextDestination.IsSmall || CanBeVisitedV2(currentPath, nextDestination, out newEqSmallCount))
                        {
                            var length = currentPath.Points.Length;
                            var newPoints = new Cave[length + 1];
                            currentPath.Points.CopyTo(newPoints, 0);
                            newPoints[length] = nextDestination;
                            possiblePaths.Push(new CavePath(newPoints, newEqSmallCount));
                        }
                    }
                }
            }

            return numberOfPaths;
        }

        private static bool CanBeVisitedV2(CavePath currentPath, Cave next, out bool newSmallCount)
        {
            newSmallCount = currentPath.HasTwoEqSmall;
            if (!currentPath.Points.Contains(next))
            {
                return true;
            }
            else if (!currentPath.HasTwoEqSmall)
            {
                newSmallCount = true;
                return true;
            }

            return false;
        }

        private static bool CanBeVisited(List<(string, bool)> currentPath, (string, bool) next)
        {
            return !currentPath.Contains(next) || currentPath.Where(v => v.Item2).GroupBy(c => c).Any(g => g.Count() > 1) == false;
        }


    }

    public static class Day12Extensions
    {
        public static Dictionary<string, List<string>> GetPathsDay12(this string[] pathStrings)
        {
            return pathStrings
                .Select(l => l.Split('-', StringSplitOptions.RemoveEmptyEntries))
                .SelectMany(path => new List<(string, string)>() { (path[0], path[1]), (path[1], path[0]) })
                .Where(pair => pair.Item1 != "end" && pair.Item2 != "start")
                .GroupBy(path => path.Item1)
                .ToDictionary(g => g.Key, g => g.Select(i => i.Item2).ToList());

        }
    }
}