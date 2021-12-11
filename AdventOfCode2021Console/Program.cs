using AdventOfCode2021;
using System.Diagnostics;

namespace AdventOfCode2021Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*** AdventOfCode2021 ***");
            int day = 0;
            ExecuteDay(new Day01(), ++day, "Sonar Sweep");
            ExecuteDay(new Day02(), ++day, "Dive!");
            ExecuteDay(new Day03(), ++day, "Binary Diagnostic");
            ExecuteDay(new Day04(), ++day, "Giant Squid");
            ExecuteDay(new Day05(), ++day, "Hydrothermal Venture");
            ExecuteDay(new Day06(), ++day, "Lanternfish");
            ExecuteDay(new Day07(), ++day, "The Treachery of Whales");
            ExecuteDay(new Day08(), ++day, "Seven Segment Search");
            ExecuteDay(new Day09(), ++day, "Smoke Basin");
            ExecuteDay(new Day10(), ++day, "Syntax Scoring");
            ExecuteDay(new Day11(), ++day, "Dumbo Octopus");
        }

        private static void ExecuteDay<T1, T2>(IDay<T1, T2> day, int dayNumber, string title, string titleProblem1 = "1", string titleProblem2 = "2")
        {
            ExecuteSolution(GetTitle(dayNumber, 1,title ), day.ExecutePart1);
            ExecuteSolution(GetTitle(dayNumber, 2,title), day.ExecutePart2);
        }

        private static void ExecuteSolution<T>(string title, Func<T> solution)
        {
            ExecuteSolution(title, () => solution?.Invoke()?.ToString() ?? "");
        }

        private static void ExecuteSolution(string title, Func<string> solution)
        {
            Stopwatch clock = new();
            clock.Start();
            var result = solution.Invoke().ToString();
            clock.Stop();
            string separator = "|";

            Console.WriteLine($"{title?.PadRight(20)}{separator}{result.PadLeft(15)}{separator}{CalculateMilliseconds(clock).PadLeft(10)}{separator}");
        }

        private static string GetTitle(int dayNumber, int problem, string title )
        {
            return $"Day {dayNumber.ToString().PadLeft(2)}.{problem.ToString().PadRight(1)} {title.PadRight(25)}";
        }

        private static string CalculateMilliseconds(Stopwatch stopwatch)
        {
            return ($"{(1000 * stopwatch.ElapsedTicks / (double)Stopwatch.Frequency):F2}ms");
        }
    }
}